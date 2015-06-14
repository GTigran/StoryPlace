using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StoryPlace.DataLayer.Base;
using StoryPlace.DataLayer.Core.Tools;

namespace StoryPlace.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Unit of Work
        protected UnitOfWork UnitOfWork;
        #endregion

        #region UserInfo

        protected int UserID
        {
            get
            {
                return  User !=null ?
                    User.Identity.GetUserId<int>() : 0;
            }
        }



        /// <summary>
        /// Indictaes wheter user is authentificated or not.
        /// </summary>
        public bool IsAutentificated
        {
            get
            {
                return User.Identity.IsAuthenticated;
            }
        }


        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Save();
            UnitOfWork.Dispose();
            
            base.Dispose(disposing);
        }
        

        #endregion


        public BaseController()
        {
            UnitOfWork = new UnitOfWork();
        }
        public BaseController(UnitOfWork unitOfWork )
        {
            UnitOfWork = unitOfWork;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            
            UnitOfWork.UserID = UserID;
        }

    }
}