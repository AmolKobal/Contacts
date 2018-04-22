using ContactDemo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ContactDemo.Controllers
{
    public class ContactController : Controller
    {
        ContactRepository _contact = new ContactRepository();

        public ActionResult Index()
        {
            var Contacts = _contact.GetAllContacts();
            return View(Contacts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _contact.Create(contact);

                    ViewBag.Message = "Contact added successfuly";

                }
                catch (Exception ex)
                {
                    Common.LogError(ModuleType.Create, ex);
                }

                return RedirectToAction("Index");
            }

            return View(contact);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contact editContact = _contact.GetAllContacts().FirstOrDefault(contct => contct.ID == id);

            if (editContact == null)
                return HttpNotFound();

            return View(editContact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _contact.Edit(contact);
                }
                catch (Exception ex)
                {
                    Common.LogError(ModuleType.Edit, ex);
                }

                return RedirectToAction("Index");
            }

            return View(contact);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contact deleteContact = _contact.GetAllContacts().FirstOrDefault(contct => contct.ID == id);

            if (deleteContact == null)
                return HttpNotFound();

            return View(deleteContact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Contact contact)
        {
            try
            {
                _contact.Delete(contact);

                ViewBag.Message = "Contact deleted successfuly";

            }
            catch (Exception ex)
            {
                Common.LogError(ModuleType.Delete, ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contact detailContact = _contact.GetAllContacts().FirstOrDefault(contct => contct.ID == id);

            if (detailContact == null)
                return HttpNotFound();

            return View(detailContact);
        }

    }
}