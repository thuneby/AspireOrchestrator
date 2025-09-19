using AspireOrchestrator.Core.Models;
using AspireOrchestrator.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Validation.Ui.Controllers
{
    public class GuidModelBaseController<T>(GuidRepositoryBase<T> repository, ILoggerFactory loggerFactory) : Controller
        where T : GuidModelBase
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<GuidRepositoryBase<T>>();

        public IActionResult Index()
        {
            return View(null, repository.GetQueryList());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = repository.Get(id.Value);
            if (entity == null)
            {
                return NotFound();
            }

            return View(null, entity);
        }

        // Get
        public IActionResult Create()
        {
            return View(null);
        }

        // POST: Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromBody] T entity)
        {
            if (ModelState.IsValid)
            {
                repository.Add(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(null, entity);
        }

        // GET: ReceiptDetail/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = repository.Get(id.Value);
            if (entity == null)
            {
                return NotFound();
            }
            return View(null, entity);
        }

        // POST: ReceiptDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [FromBody] T entity)
        {
            if (id != entity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repository.Update(entity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptDetailExists(entity.Id))
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
            return View(null, entity);
        }

        // GET: ReceiptDetail/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = repository.Get(id.Value);
            if (entity == null)
            {
                return NotFound();
            }

            return View(null, entity);
        }

        // POST: ReceiptDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var entity = repository.Get(id);
            if (entity != null)
            {
                repository.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptDetailExists(Guid id)
        {
            return repository.Get(id) != null;
        }
    }
}
