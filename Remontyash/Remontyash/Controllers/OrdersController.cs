using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Remontyash.Models;

namespace Remontyash.Controllers 
{
    [Authorize] //Мы можем работать с этим контроллером только при авторизации
    public class OrdersController : Controller //объявляется что это контроллер
    {
        private readonly RemontDBContext _context;//создается переменная для работы с базой данных

        public OrdersController(RemontDBContext context) 
        {
            _context = context;
        }

        public async Task<IActionResult> Index()//открытие главной страницы
        {
            var remontDBContext = _context.Orders.Include(o => o.Client).Include(o => o.Emp).Include(o => o.TypeJob);//Получение данных из БД
            return View(await remontDBContext.ToListAsync());//Возвращение представления с данными
        }

        public async Task<IActionResult> Details(Guid? id)//Переход на страницу с деталями заказа
        {
            if (id == null)//Если ай ди не найден то мы выдаем  NotFound
            {
                return NotFound();
            }

            var order = await _context.Orders//Получение данных из БД по ай ди
                .Include(o => o.Client)
                .Include(o => o.Emp)
                .Include(o => o.TypeJob)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)//Если ничего не найдено то мы выдаем  NotFound
            {
                return NotFound();
            }

            return View(order);//Возвращение представления с найденным значением
        }

        public IActionResult Create()//Возвращение представления страниц с созданием
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Fio"); //Занесение переменных в ViewData из БД
            ViewData["Empid"] = new SelectList(_context.Emps, "Empid", "Fio");
            ViewData["TypeJobId"] = new SelectList(_context.TypeJobs, "TypeJobId", "Description"); 
            ViewData["TypeTech"] = new SelectList(_context.TypeTechnics, "TypeTechnicId", "Description");
            return View();//Возвращение представление
        }

        [HttpPost]//Пост метод
        [ValidateAntiForgeryToken]//предназначен для противодействия подделке межсайтовых запросов, производя верификацию токенов
        public async Task<IActionResult> Create([Bind("OrderId,TypeJobId,ClientId,Empid,Accepted,Completed,IsCompleted")] Order order)//Получаем данные из страницы create
        {
            if (ModelState.IsValid)//Проверяем валидность модели
            {
                order.OrderId = Guid.NewGuid();//Создаем новый ай ди
                _context.Add(order);//Добавляем новый заказ в БД
                await _context.SaveChangesAsync();//Сохраняем изменения
                return RedirectToAction(nameof(Index));//Отправляем на главную страницу (index)
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Fio", order.ClientId);//Занесение переменных в ViewData из БД
            ViewData["Empid"] = new SelectList(_context.Emps, "Empid", "Fio", order.Empid);//Занесение переменных в ViewData из БД
            ViewData["TypeJobId"] = new SelectList(_context.TypeJobs, "TypeJobId", "Description", order.TypeJobId);//Занесение переменных в ViewData из БД
            return View(order);//Возвращение представления со значением
        }

        public async Task<IActionResult> Edit(Guid? id)//Переход на страницу редактирования
        {
            if (id == null)//Если ничего не найдено то мы выдаем  NotFound
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);//Поис заказа по ай ди из БД
            if (order == null)//Если ничего не найдено то мы выдаем  NotFound
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Fio", order.ClientId);//Занесение переменных в ViewData из БД
            ViewData["Empid"] = new SelectList(_context.Emps, "Empid", "Fio", order.Empid);//Занесение переменных в ViewData из БД
            ViewData["TypeJobId"] = new SelectList(_context.TypeJobs, "TypeJobId", "Description", order.TypeJobId);//Занесение переменных в ViewData из БД
            return View(order);//Возвращение представления со значением
        }

        [HttpPost]//Пост метод
        [ValidateAntiForgeryToken]//предназначен для противодействия подделке межсайтовых запросов, производя верификацию токенов
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderId,TypeJobId,ClientId,Empid,Accepted,Completed,IsCompleted")] Order order)//Функция для редактирования
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Fio", order.ClientId);
            ViewData["Empid"] = new SelectList(_context.Emps, "Empid", "Fio", order.Empid);
            ViewData["TypeJobId"] = new SelectList(_context.TypeJobs, "TypeJobId", "Description", order.TypeJobId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Emp)
                .Include(o => o.TypeJob)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<List<TypeJob>> GetTypeJobs(Guid id)
        {
            var objTypeJobs = await _context.TypeJobs.Where(a => a.TypeTechnicId == id).ToListAsync();
            return objTypeJobs;
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
