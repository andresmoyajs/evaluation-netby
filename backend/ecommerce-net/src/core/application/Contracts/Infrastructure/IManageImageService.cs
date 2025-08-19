using application.Models.ImageManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Contracts.Infrastructure
{
    public interface IManageImageService
    {
        Task<ImageResponse> UploadImage(ImageData imageStream);
    }
}
