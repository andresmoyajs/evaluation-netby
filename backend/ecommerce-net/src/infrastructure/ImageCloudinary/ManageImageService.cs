using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using application.Contracts.Infrastructure;
using application.Models.ImageManagment;

namespace infrastructure.ImageCloudinary
{
    public class ManageImageService : IManageImageService
    {
        private CloudinarySettings cloudinarySettings;

        public ManageImageService(IOptions<CloudinarySettings> cloudinarySettings)
        {
            this.cloudinarySettings = cloudinarySettings.Value;
        }
        public async Task<ImageResponse> UploadImage(ImageData imageStream)
        {
            var account = new Account(
                cloudinarySettings.CloudName,
                cloudinarySettings.ApiKey,
                cloudinarySettings.ApiSecret
                );

            var cloudinary = new Cloudinary(account);
            var uploadImage = new ImageUploadParams()
            {
                File = new FileDescription(imageStream.Nombre, imageStream.ImageStream)
            };

            var uploadResult = await cloudinary.UploadAsync(uploadImage);

            if (uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return new ImageResponse
                {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.Url.ToString()
                };
            }


            throw new Exception("No se pudo guardar la imagen");
        }
    }
}
