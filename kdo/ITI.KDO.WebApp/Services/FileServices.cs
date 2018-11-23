using ITI.KDO.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public enum EType
    {
        User = 0,
        Present = 1,
        Event = 2
    }

    public class FileServices
    {
        readonly UserGateway _userGateway;
        readonly PresentGateway _presentGateway;
        readonly EventGateway _eventGateway;


        public FileServices(UserGateway userGateway, PresentGateway presentGateway, EventGateway eventGateway)
        {
            _userGateway = userGateway;
            _presentGateway = presentGateway;
            _eventGateway = eventGateway;
        }

        //private byte[] BuildByteArray( List<IFormFile> files )
        //{
        //    byte[] coverImageBytes = null;

        //    if( files != null && files.Count != 0 )
        //    {
        //        foreach (var file in files)
        //        {
        //            BinaryReader reader = new BinaryReader(file.OpenReadStream());
        //            coverImageBytes = reader.ReadBytes((int)file.Length);
        //        }
        //        return coverImageBytes;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public Result UpdatePicture(int id, List<IFormFile> files, EType typeOfPicture)
        //{

        //    var picture = BuildByteArray(files);
        //    return picture == null ? UpdatePicture(id, null, typeOfPicture) : UpdatePicture(id, picture, typeOfPicture);
        //}

        public readonly Dictionary<int, byte[]> ListOfFiles = new Dictionary<int, byte[]>();
        
        public void SavePictureSnapshot(int id, List<IFormFile> files)
        {
            if (files != null && files.Count != 0)
                foreach (var file in files)
                    using (BinaryReader reader = new BinaryReader(file.OpenReadStream()))
                        ListOfFiles[id] = reader.ReadBytes((int)file.Length);
        }

        public bool TryUpdatePicture(int id, EType typeOfFile)
        {
            try
            {        
                if (ListOfFiles.TryGetValue(id, out var file))
                {
                    UpdatePicture(id, file, typeOfFile);
                
                }
                return true;
            }

            catch ( Exception e)
            {
                return false;
            }
        }

        public Result UpdatePicture(int id, byte[] files, EType typeOfPicture)
        {
            User user;
            user = _userGateway.FindById(id);

            switch (typeOfPicture){

                case EType.User:
                    if (files == null)
                    {
                        ListOfFiles[id] = File.ReadAllBytes(@"..\ITI.KDO.WebApp\App\kdo\src\assets\unknow.jpg");
                        _userGateway.UserSetPhoto(id, ListOfFiles[id]);
                    }
                    else if (ListOfFiles[id] != null)
                        _userGateway.UserSetPhoto(id, ListOfFiles[id]);
                    return Result.Success(Status.Ok);
                case EType.Present:
                    if (ListOfFiles[id] == null)
                        ListOfFiles[id] = File.ReadAllBytes(@"..\ITI.KDO.WebApp\App\kdo\src\assets\present.jpg");
                    else if (ListOfFiles[id] != null)
                        _presentGateway.PresentSetPicture(id, ListOfFiles[id]);
                    // TODO: Remove this useless line
                    //var present = _presentGateway.FindByPresentId(id);
                    return Result.Success(Status.Ok);
                case EType.Event:
                    if (ListOfFiles[id] == null)
                        ListOfFiles[id] = File.ReadAllBytes(@"..\ITI.KDO.WebApp\App\kdo\src\assets\event.jpg");
                    else if (ListOfFiles[id] != null)
                        _eventGateway.EventSetPicture(id, ListOfFiles[id]);
                    break;
            }
            return Result.Success(Status.Ok);
        }
    }
}
