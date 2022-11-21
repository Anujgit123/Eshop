using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Identity.Constants;
using Ecommerce.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.CustomerAccount.Commands
{
    public class RegisterCustomerCommand : IRequest<Response<UserIdentityDto>>
    {
        public CustomerRegisterDto CustomerRegister { get; set; }
    }
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, Response<UserIdentityDto>>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IKeyAccessor _keyAccessor;
        private readonly ICurrentUser _currentUser;
        public RegisterCustomerCommandHandler(IDataContext db, IMapper mapper, UserManager<ApplicationUser> userManager, IKeyAccessor keyAccessor, ICurrentUser currentUser)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _keyAccessor = keyAccessor;
            _currentUser = currentUser;
        }

        public async Task<Response<UserIdentityDto>> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var timeNow = DateTime.UtcNow;
            var conAdv = _keyAccessor["AdvancedConfiguration"] != null ? JsonSerializer.Deserialize<AdvancedConfigurationDto>(_keyAccessor["AdvancedConfiguration"]) : new AdvancedConfigurationDto();
            var user = _mapper.Map<ApplicationUser>(request.CustomerRegister);
            user.CreatedBy = _currentUser.UserName;
            user.CreatedDate = timeNow;
            user.LastModifiedBy = _currentUser.UserName;
            user.LastModifiedDate = timeNow;
            user.Gender = Gender.Unknown.ToString();
            user.IsActive = true;

            if (user.Email != null && await _userManager.FindByEmailAsync(user.Email) != null)
            {
                return Response<UserIdentityDto>.Fail("Email already exists! Please try different one!");
            }

            if (user.UserName != null && await _userManager.FindByNameAsync(user.UserName) != null)
            {
                return Response<UserIdentityDto>.Fail("Username already exists! Please try different one!");
            }
            try
            {
                var rs = await _userManager.CreateAsync(user, request.CustomerRegister.Password);
                if (rs.Succeeded)
                {
                    //Add to Customer
                    var customer = new Customer();
                    customer.FullName = user.FirstName != null ? user.FirstName + " " + user.LastName : null;
                    customer.Email = user.Email;
                    customer.ApplicationUserId = user.Id;

                    await _db.Customers.AddAsync(customer, cancellationToken);
                    await _db.SaveChangesAsync(cancellationToken);

                    //Add role to Customer
                    var currentUser = await _userManager.FindByNameAsync(user.UserName);
                    var roleresult = await _userManager.AddToRoleAsync(currentUser, DefaultApplicationRoles.Customer);

                    return Response<UserIdentityDto>.Success(new UserIdentityDto { Id = user.Id }, rs.ToString());
                }
                else
                {
                    return Response<UserIdentityDto>.Fail(rs.ToString());
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
