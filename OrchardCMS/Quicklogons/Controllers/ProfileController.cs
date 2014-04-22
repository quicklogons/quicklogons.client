using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Mvc;
using Orchard.Security;
using Orchard.Themes;
using Orchard.Users.Models;
using Quicklogons.Client;
using Quicklogons.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quicklogons.Controllers
{
    [HandleError, Themed, ValidateInput(false)]
    [OrchardFeature("Quicklogons")]
    public class ProfileController : Controller, IUpdateModel
    {
        private readonly IMembershipService _membershipService;
        private readonly IOrchardServices _orchardServices;

        private IUser CurrentUser { get { return _orchardServices.WorkContext.CurrentUser; } }
        public Localizer T { get; set; }

        public ProfileController(IMembershipService membershipService, IOrchardServices orchardServices)
        {
            _membershipService = membershipService;
            _orchardServices = orchardServices;
        }

        public ActionResult Index()
        {
            if (CurrentUser == null) return new HttpUnauthorizedResult();

            dynamic shape = _orchardServices.ContentManager.BuildDisplay(CurrentUser.ContentItem);

            return View((object)shape);
        }

        public ActionResult Edit()
        {
            if (CurrentUser == null) return new HttpUnauthorizedResult();

            dynamic shape = _orchardServices.ContentManager.BuildEditor(CurrentUser.ContentItem);

            return View((object)shape);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(string returnUrl)
        {
            if (CurrentUser == null) return new HttpUnauthorizedResult();

            dynamic shape = _orchardServices.ContentManager.UpdateEditor(CurrentUser, this);
            if (!ModelState.IsValid)
            {
                _orchardServices.TransactionManager.Cancel();
                return View("Edit", (object)shape);
            }

            _orchardServices.Notifier.Add(Orchard.UI.Notify.NotifyType.Information, T("Your profile has been saved."));

            if (!string.IsNullOrEmpty(returnUrl)) return Redirect(Request.Url.ValidateLocalUrl(returnUrl));

            return RedirectToAction("Edit");
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }
    }
}