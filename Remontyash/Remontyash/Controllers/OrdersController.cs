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
    [Authorize]  
    public class OrdersController : Controller //объявляется что это контроллер создаем класс и наследуем его от контроллера
    {
        private readonly RemontDBContext _context;//создается переменная для работы с базой данных

        public OrdersController(RemontDBContext context) //Присваивание переменной базы данных
        {
            _context = context;
        }

        public async Task<IActionResult> Index()//открытие главной страницы
        {
            var remontDBContext = _context.Orders.Include(o => o.Client).Include(o => o.Emp).Include(o => o.TypeJob);//Получение данных из БД на сайт
            return View(await remontDBContext.ToListAsync());//Возвращение представления с данными
        }

        public async Task<IActionResult> Details(Guid? id)//Переход на страницу с деталями заказа
        {
            if (id == null)//Если ай ди не найден то мы выдаем  NotFound
            {
                return NotFound();
            }

            var order = await _context.Orders//Получение данных из БД по id
                .Include(o => o.Client)//Включает в себя Клиентов итд
                .Include(o => o.Emp)
                .Include(o => o.TypeJob)
                .FirstOrDefaultAsync(m => m.OrderId == id);//Ищет заказ по id 
            if (order == null)//Если ничего не найдено то мы выдаем  NotFound
            {
                return NotFound();
            }

            return View(order);//Возвращем представление с найденным значением
        }

        public IActionResult Create()//Возвращение представления(переход на страницу) страниц с созданием заказа
        { 
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Fio"); //Занесение переменных в ViewData() из БД
            ViewData["Empid"] = new SelectList(_context.Emps, "Empid", "Fio");//ViewData выбор сотрудников в окне создания предлженных
            ViewData["TypeJobId"] = new SelectList(_context.TypeJobs, "TypeJobId", "Description"); //_context.()-берем данные из БД
            ViewData["TypeTech"] = new SelectList(_context.TypeTechnics, "TypeTechnicId", "Description");
            return View();//Возвращение представление
        }

        [HttpPost]//Пост метод позволяет принимать данные с формы
        [ValidateAntiForgeryToken]//предназначен для противодействия подделке межсайтовых запросов, производя верификацию токенов
        public async Task<IActionResult> Create(Order order)//Получаем данные из страницы create
        {
            order.Accepted = DateTime.Now;
            if (order.Completed == null) order.IsCompleted = false;
            if (ModelState.IsValid)//Проверяем валидность модели (проверка на правильность модели)
            {
                order.OrderId = Guid.NewGuid();//Создаем новый id
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

            var order = await _context.Orders.FindAsync(id);//Поис заказа по id из БД
            if (order == null)//Если ничего не найдено то мы выдаем  NotFound
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Fio", order.ClientId);//Занесение переменных в ViewData из БД
            ViewData["Empid"] = new SelectList(_context.Emps, "Empid", "Fio", order.Empid);//Занесение переменных в ViewData из БД
            ViewData["TypeJobId"] = new SelectList(_context.TypeJobs, "TypeJobId", "Description", order.TypeJobId);//Занесение переменных в ViewData из БД
            return View(order);//Возвращение представления со значением
        }

        [HttpPost]//Пост метод позволяет принимать данные с формы
        [ValidateAntiForgeryToken]//предназначен для противодействия подделке межсайтовых запросов, производя верификацию токенов
        public async Task<IActionResult> Edit(Guid id, Order order)//Функция для редактирования  
        {
            
            if (id != order.OrderId)
            {
                return NotFound();
            }
            order.Completed = DateTime.Now;
            order.IsCompleted = true;
            if (ModelState.IsValid)//Проверка на валидность(правильность) у нее заполнены все поля
            {
                try
                {
                    _context.Update(order);//редактирование данные о заказе
                    await _context.SaveChangesAsync();//сохранение изменений
                }
                catch (DbUpdateConcurrencyException)//если есть какая то ошибка то мы выводим not found
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
                return RedirectToAction(nameof(Index));//возвращение на главную страницу
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Fio", order.ClientId);
            ViewData["Empid"] = new SelectList(_context.Emps, "Empid", "Fio", order.Empid);
            ViewData["TypeJobId"] = new SelectList(_context.TypeJobs, "TypeJobId", "Description", order.TypeJobId);
            return View(order);
        }

        public async Task<IActionResult> Delete(Guid? id)//переход на страницу удаления
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders//Получаем данные их БД и записываем их в заказ
                .Include(o => o.Client)
                .Include(o => o.Emp)
                .Include(o => o.TypeJob)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);//Переходим на страницу удаления
        }

        [HttpPost, ActionName("Delete")]//Пост метод удаления(получает данные)
        [ValidateAntiForgeryToken]//предназначен для противодействия подделке межсайтовых запросов, производя верификацию токенов
        public async Task<IActionResult> DeleteConfirmed(Guid id)//Метод подтверждения удаления
        {
            var order = await _context.Orders.FindAsync(id);//Берем данные заказа
            _context.Orders.Remove(order);//Удаляем данные заказа
            await _context.SaveChangesAsync();//Сохраняем изменения в БД
            return RedirectToAction(nameof(Index));//Переход на главную страницу
        }
        public async Task<List<TypeJob>> GetTypeJobs(Guid id)//Метод выбора работы по id
        {
            var objTypeJobs = await _context.TypeJobs.Where(a => a.TypeTechnicId == id).ToListAsync();//Берем из БД все данные о типе работы в которых тип техники соответствует id
            return objTypeJobs;//Возвращаем тип работ
        }

        private bool OrderExists(Guid id)//Метод проверки существования заказа
        {
            return _context.Orders.Any(e => e.OrderId == id);//Ищем заказ по id в БД
        }
    }
}
