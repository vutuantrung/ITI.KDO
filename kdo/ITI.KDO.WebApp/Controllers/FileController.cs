using ITI.KDO.DAL;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Models.UserViewModels;
using ITI.KDO.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class FileController : Controller
    {
        readonly FileServices _fileServices;

        public FileController(FileServices fileServices)
        {
            _fileServices = fileServices;
        }

        /// <summary>
        ///  Update Image User by ID (Controller)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost("img/{id}")]
        public object UpdateImageUser(int id, List<IFormFile> files)
        {
            //Result result = _fileServices.UpdatePicture(id, file);
            //return this.CreateResult(result);

            try
            {
                _fileServices.SavePictureSnapshot(id, files);
                return Ok();
            }
            catch (Exception)
            {
                return false;
            }


        }

        [HttpPost("{typeOfFile}/{id}")]
        public object TypeOfFile(int id, EType typeOfFile)
        {
            try
            {
                _fileServices.TryUpdatePicture(id, typeOfFile);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{typeOfFile}/{id}")]
        public object DeleteImage(int id, EType typeOfFile)
        {
            try
            {
                _fileServices.UpdatePicture(id, null, typeOfFile);
                return Ok();
            }
            catch( Exception )
            {
                return BadRequest();
            }
        }
    }

}


