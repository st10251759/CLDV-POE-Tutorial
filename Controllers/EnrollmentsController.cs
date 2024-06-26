﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cloud_MVC_Tutorial.Data;
using Cloud_MVC_Tutorial.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Cloud_MVC_Tutorial.ViewModels;

namespace Cloud_MVC_Tutorial.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly Cloud_MVC_TutorialContext _context;

        public EnrollmentsController(Cloud_MVC_TutorialContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var cloud_MVC_TutorialContext = _context.Enrollment.Include(e => e.Course).Include(e => e.Student);
            return View(await cloud_MVC_TutorialContext.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseId");
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentId,CourseId,StudentId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                //Set the enrollment date when creating a new enrollment
                enrollment.EnrollmentDate = DateTime.Now;
                enrollment.ModifiedDate = DateTime.Now; //Initially, Modified is the same as EnrollmentDate - will change in the  Update View

                _context.Add(enrollment);
                await _context.SaveChangesAsync();

                //Add to history
                var history = new EnrollmentHistory
                {
                    EnrollmentId = enrollment.EnrollmentId,
                    ChangeDate = DateTime.Now,
                };

                _context.EnrollmentHistories.Add(history);

                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseId", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", enrollment.StudentId);
            return View(enrollment);
        }

        public async Task<IActionResult> EnrollmentHistory(EnrollmentHistoryViewModel model)
        {
            var query = _context.Enrollment.Include(e => e.Course).Include(e => e.Student).AsQueryable();
            if (!string.IsNullOrEmpty(model.FilterCourseName))
            {
                query = query.Where(e => e.Course.CourseName.Contains(model.FilterCourseName));
            }
            if (!string.IsNullOrEmpty(model.FilterStudentName))
            {
                query = query.Where(e => e.Student.StudentName.Contains(model.FilterStudentName));
            }
            if (model.FilterStartDate.HasValue)
            {
                query = query.Where(e => e.EnrollmentDate >= model.FilterStartDate.Value);
            }
            if (model.FilterEndDate.HasValue)
            {
                query = query.Where(e => e.EnrollmentDate <= model.FilterEndDate.Value);
            }

            model.Enrollments = await query.OrderByDescending(e => e.ModifiedDate).ToListAsync();

            return View(model); // Add this return statement
        }

        private bool EnrollmentExsists(int id)
        {
            return _context.Enrollment.Any(equals => equals.EnrollmentId == id);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseId", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentId,CourseId,StudentId")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseId", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollment.Remove(enrollment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollment.Any(e => e.EnrollmentId == id);
        }
    }
}
