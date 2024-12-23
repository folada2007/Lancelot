using SkiaSharp;

namespace Lancelot.Client.Services.Resourse
{
    public class ResourceLoader
    {
        private readonly HttpClient _httpClient;

        public ResourceLoader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SKBitmap> LoadBitmapAsync(string path) 
        {
            var BytesArray = await _httpClient.GetByteArrayAsync(path);
            var Decode = SKBitmap.Decode(BytesArray);

            return Decode;
        }
    }
}
