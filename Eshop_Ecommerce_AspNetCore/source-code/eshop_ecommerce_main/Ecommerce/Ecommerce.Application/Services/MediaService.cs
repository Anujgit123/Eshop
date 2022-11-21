using AutoMapper;
using Ecommerce.Application.Common;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class MediaService : IMediaService
    {
        private readonly IDataContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public MediaService(IDataContext db, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IMapper mapper)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<string> FileUploadAsync(FileUploadDto fileUpload)
        {
            try
            {
                string fileName = null;
                string filePath = _configuration.GetValue<string>("MediaManager:MediaPath");
                string fileId = Guid.NewGuid().ToString();
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, filePath);
                var timeNow = DateTime.Now;

                if (fileUpload.File != null)
                {
                    fileName = fileUpload.FileName ?? Guid.NewGuid().ToString();
                    fileName += Path.GetExtension(fileUpload.File.FileName).ToLower();
                    using (var fileStream = new FileStream(Path.Combine(uploadsFolder, fileName), FileMode.Create))
                    {
                        fileUpload.File.CopyTo(fileStream);
                    }
                }

                Gallery fileStore = new()
                {
                    Id = fileId,
                    Name = filePath + "/" + fileName
                };
                await _db.Galleries.AddAsync(fileStore);
                await _db.SaveChangesAsync();

                return fileStore.Id;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<GalleryDto>> GetAsync()
        {
            var filelist = await _db.Galleries.AsNoTracking().OrderByDescending(o => o.LastModifiedDate).ToListAsync();
            var result = _mapper.Map<List<GalleryDto>>(filelist);
            return result;
        }

        public async Task<GalleryDto> GetByIdAsync(string id)
        {
            var file = await _db.Galleries.Where(o => o.Id == id).AsNoTracking().FirstOrDefaultAsync();
            var result = _mapper.Map<GalleryDto>(file);
            return result;
        }

        public async Task<IEnumerable<GalleryDto>> GetPagedAsync(int pageIndex, int pageSize)
        {
            var getPagedGalleries = await _db.Galleries.OrderByDescending(o => o.LastModifiedDate)
                                                .Skip(pageIndex * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync();
            var result = _mapper.Map<List<GalleryDto>>(getPagedGalleries);
            return result;
        }

        public async Task<string> RemoveAsync(string id)
        {
            var file = await _db.Galleries.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            //string filesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "gallery");
            //string filePath = Path.Combine(filesFolder.Replace("/", "\\"), file.Name);

            string filesFolder = Path.Combine(_webHostEnvironment.WebRootPath, file.Name);
            string filePath = filesFolder.Replace("/", "\\");

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            _db.Galleries.Remove(file);
            await _db.SaveChangesAsync();
            return file.Id;
        }

        public async Task<GalleryDto> UpdateAsync(GalleryDto galleryDto)
        {
            var file = await _db.Galleries.AsNoTracking().FirstOrDefaultAsync(x => x.Id == galleryDto.Id);
            file.Title = galleryDto.Title;
            file.Tags = galleryDto.Tags;

            _db.Galleries.Update(file);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GalleryDto>(file);
            return result;
        }
    }
}
