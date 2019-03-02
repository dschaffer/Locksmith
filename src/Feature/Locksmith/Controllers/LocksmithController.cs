using Locksmith.Models;
using Sitecore.Data.Items;
using System.Web.Mvc;

namespace Locksmith.Controllers
{
    public class LocksmithController : Controller
    {
        [HttpPost]
        public JsonResult SaveAndUnlock(UnlockRequest model)
        {
            if (ModelState.IsValid)
            {
                SecurityController security = new SecurityController();
                Item item = Dbs.Db.GetItem(model.Id);

                if (item != null && security.IsUnlockable(item))
                {
                    if (model.RejectedFields != null && model.RejectedFields.Length > 0)
                    {
                        // revert changes
                    }

                    using (new EditContext(item))
                        item.Locking.Unlock();

                    return Json(new { success = true, message = "ok" });
                }
                else
                    return Json(new { success = false, message = "invalid item" });
            }
            else
                return Json(new { success = false, message = "invalid request" });
        }
    }
}