﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoreServiceCollection.Core.Services;
using CoreServiceCollection.Domain.Models;
using CoreServiceCollection.Localisation.Areas.Person.Models;
using Microsoft.AspNetCore.Mvc;
using IMapper = AutoMapper.IMapper;

namespace CoreServiceCollection.Localisation.Areas.Person.Controllers
{
    [Area("Person")]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;

        public HomeController(IMapper mapper, IPersonService personService)
        {
            _mapper = mapper;
            _personService = personService;
        }

        // GET: Person
        public ActionResult Index()
        {
            var persons = _mapper.Map<IList<PersonViewModel>>(_personService.Persons);

            return View(persons);
        }

        // GET: Person/Details/5
        public ActionResult Details(Guid id)
        {
            var person = GetPersonAsViewModel(GetPersonById(id));
            return View(person);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonViewModel viewModel)
        {
            try
            {
                viewModel.Id = Guid.NewGuid();
                _personService.Persons.Add(GetPersonAsModel(viewModel));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Edit/5
        public ActionResult Edit(Guid id)
        {
            var person = GetPersonAsViewModel(GetPersonById(id));
            return View(person);
        }

        // POST: Person/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonViewModel viewModel)
        {
            try
            {
                var person = GetPersonById(viewModel.Id.GetValueOrDefault(Guid.Empty));
                _personService.Persons.Remove(person);

                _personService.Persons.Add(GetPersonAsModel(viewModel));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        public ActionResult Delete(Guid id)
        {
            var person = GetPersonAsViewModel(GetPersonById(id));
            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PersonViewModel viewModel)
        {
            try
            {
                var person = GetPersonById(viewModel.Id.GetValueOrDefault(Guid.Empty));
                _personService.Persons.Remove(person);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private PersonModel GetPersonById(Guid id)
        {
            return _personService.Persons.First(p => p.Id == id);
        }

        private PersonViewModel GetPersonAsViewModel(PersonModel person)
        {
            return _mapper.Map<PersonViewModel>(person);
        }

        private PersonModel GetPersonAsModel(PersonViewModel person)
        {
            return _mapper.Map<PersonModel>(person);
        }
    }
}