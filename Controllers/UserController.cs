using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // GET: User
    public ActionResult Index()
    {
        // Implement the Index method here
        return View(userlist);
    }

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        // Busca el usuario en la lista por su ID
        var user = userlist.FirstOrDefault(u => u.Id == id);

        // Si el usuario no es encontrado, devuelve un resultado de "No encontrado"
        if (user == null)
        {
            return NotFound();
        }

        // Si el usuario es encontrado, devuelve la vista Details con el modelo de usuario
        return View(user);
    }

    // GET: User/Create
    public ActionResult Create()
    {
        // Devuelve la vista Create
        return View();
    }

    // POST: User/Create
    [HttpPost]
    public ActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            // Agrega el nuevo usuario a la lista
            userlist.Add(user);

            // Redirige a la acción Index para mostrar la lista actualizada de usuarios
            return RedirectToAction("Index");
        }

        // Si el modelo no es válido, devuelve la vista Create con el modelo actual
        return View(user);
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        // Busca el usuario en la lista por su ID
        var user = userlist.FirstOrDefault(u => u.Id == id);

        // Si el usuario no es encontrado, devuelve un resultado de "No encontrado"
        if (user == null)
        {
            return NotFound();
        }

        // Si el usuario es encontrado, devuelve la vista Edit con el modelo de usuario
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
        // It receives user input from the form submission and updates the corresponding user's information in the userlist.
        // If successful, it redirects to the Index action to display the updated list of users.
        // If no user is found with the provided ID, it returns a HttpNotFoundResult.
        // If an error occurs during the process, it returns the Edit view to display any validation errors.
        if (ModelState.IsValid)
        {
            // Busca el usuario en la lista por su ID
            var existingUser = userlist.FirstOrDefault(u => u.Id == id);

            // Si el usuario no es encontrado, devuelve un resultado de "No encontrado"
            if (existingUser == null)
            {
                return NotFound();
            }

            // Actualiza la información del usuario
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;

            // Redirige a la acción Index para mostrar la lista actualizada de usuarios
            return RedirectToAction("Index");
        }

        // Si el modelo no es válido, devuelve la vista Edit con el modelo actual
        return View(user);
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        // Busca el usuario en la lista por su ID
        var user = userlist.FirstOrDefault(u => u.Id == id);

        // Si el usuario no es encontrado, devuelve un resultado de "No encontrado"
        if (user == null)
        {
            return NotFound();
        }

        // Si el usuario es encontrado, devuelve la vista Delete con el modelo de usuario
        return View(user);
    }

    // POST: User/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        // Busca el usuario en la lista por su ID
        var user = userlist.FirstOrDefault(u => u.Id == id);

        // Si el usuario no es encontrado, devuelve un resultado de "No encontrado"
        if (user == null)
        {
            return NotFound();
        }

        // Elimina el usuario de la lista
        userlist.Remove(user);

        // Redirige a la acción Index para mostrar la lista actualizada de usuarios
        return RedirectToAction("Index");
    }
}