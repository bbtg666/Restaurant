using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace Core.Helper
{
    public static class MiscHelper
    {
        public static string UploadFile(IFormFile file)
        {
            try
            {
                var id = Guid.NewGuid().ToString();
                var extention = (new FileInfo(file.FileName)).Extension;
                var fileName = Path.GetFileName($"{id}{extention}");
                string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\image", fileName);
                var stream = new FileStream(uploadpath, FileMode.Create);
                file.CopyToAsync(stream);

                return fileName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string? name = Enum.GetName(type, value);
            if (name is not null)
            {
                System.Reflection.FieldInfo? field = type.GetField(name);
                if (field is not null)
                {
                    DescriptionAttribute? attr = Attribute.GetCustomAttribute(field,
                                        typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr is not null)
                    {
                        return attr.Description;
                    }
                }
            }

            return string.Empty;
        }
    }
}
