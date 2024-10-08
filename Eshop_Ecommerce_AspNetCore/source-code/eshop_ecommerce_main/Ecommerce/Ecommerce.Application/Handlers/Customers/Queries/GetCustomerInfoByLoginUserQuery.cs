﻿using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Handlers.Customers.Queries
{
    public class GetCustomerInfoByLoginUserQuery : IRequest<CustomerDto>
    {
    }
    public class GetCustomerInfoByLoginUserQueryHandler : IRequestHandler<GetCustomerInfoByLoginUserQuery, CustomerDto>
    {
        private readonly IDataContext _db;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetCustomerInfoByLoginUserQueryHandler(IDataContext db, IMapper mapper, ICurrentUser currentUser, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _mapper = mapper;
            _currentUser = currentUser;
            _userManager = userManager;
        }

        public async Task<CustomerDto> Handle(GetCustomerInfoByLoginUserQuery request, CancellationToken cancellationToken)
        {

            var data = await _db.Customers.FirstOrDefaultAsync(o => o.ApplicationUserId == _currentUser.UserId);
            var result = _mapper.Map<CustomerDto>(data);
            return result;
        }
    }
}
