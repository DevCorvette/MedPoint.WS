using System.Threading.Tasks;

namespace MedPoint.Stores
{
    public interface IImageStore
    {
        Task SaveImage(string imageName, byte[] image);
        void DeleteImage(string imageName);
    }
}
