﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using NurseVolunteeringSystem.Models;
using NurseVolunteeringSystem;
using Microsoft.AspNetCore.Http;

public class ChronicConditionController : Controller
{
 
    private readonly AppDBContext _context;

    public ChronicConditionController(AppDBContext context)
    {
        _context = context;
    }


    [Area("Manager")]
    public IActionResult Index()
    {
        if(HttpContext.Session.GetString("Names")==null)
        {
            return RedirectToAction("Account", "Login", new { area = "" });
        }

        var chronicConditions = _context.ChronicCondition.Where(c => c.Status == "Active").ToList();
        return View(chronicConditions);
    }

    [Area("Manager")]

    public IActionResult Create()
    {
        if (HttpContext.Session.GetString("Names") == null)
        {
            return RedirectToAction("Account", "Login", new { area = "" });
        }

        return View();
    }

    [Area("Manager")]

    [HttpPost]
    public IActionResult Create(ChronicCondition model)
    {
        if (ModelState.IsValid)
        {
            _context.ChronicCondition.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(model);
    }
    [Area("Manager")]

    public IActionResult Edit(int id)
    {
        if (HttpContext.Session.GetString("Names") == null)
        {
            return RedirectToAction("Account", "Login", new { area = "" });
        }

        var condition = _context.ChronicCondition.Find(id);
        if (condition == null)
        {
            return NotFound();
        }
        return View(condition);
    }

    [Area("Manager")]

    [HttpPost]
    public IActionResult Edit(ChronicCondition model)
    {
        if (ModelState.IsValid)
        {
            _context.ChronicCondition.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(model);
    }
    [Area("Manager")]

    public IActionResult Details(int id)
    {
        if (HttpContext.Session.GetString("Names") == null)
        {
            return RedirectToAction("Account", "Login", new { area = "" });
        }

        var condition = _context.ChronicCondition.Find(id);
        if (condition == null)
        {
            return NotFound();
        }
        return View(condition);
    }

    [Area("Manager")]
    public IActionResult Delete(int id)
    {
        if (HttpContext.Session.GetString("Names") == null)
        {
            return RedirectToAction("Account", "Login", new { area = "" });
        }

        var condition = _context.ChronicCondition.Find(id);
        if (condition == null)
        {
            return NotFound();
        }
        return View(condition);
    }

    [Area("Manager")]

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var condition = _context.ChronicCondition.Find(id);
        if (condition != null)
        {
            condition.Status = "In-Active";
            _context.ChronicCondition.Update(condition);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    
}

