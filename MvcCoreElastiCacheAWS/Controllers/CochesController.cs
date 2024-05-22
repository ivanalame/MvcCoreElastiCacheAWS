using Microsoft.AspNetCore.Mvc;
using MvcCoreElastiCacheAWS.Models;
using MvcCoreElastiCacheAWS.Repositories;
using MvcCoreElastiCacheAWS.Services;

namespace MvcCoreElastiCacheAWS.Controllers
{
    public class CochesController : Controller
    {
        private ServiceAWSCache service;
        private RepositoryCoches repo; 

        public CochesController(RepositoryCoches repo,ServiceAWSCache service)
        {
            this.service = service;
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Coche>coches = this.repo.GetCoches();
            return View(coches);
        }

        public IActionResult Details(int id)
        {
            Coche coche = this.repo.FindCoche(id);
            return View(coche);
        }

        public async Task<IActionResult> SeleccionarFavorito(int idcoche)
        {
            //BUSCAMOS EL COCHE DENTRO DEL DOCUMENTO XML(REPO)
            Coche car = this.repo.FindCoche(idcoche);
            await this.service.AddCocheFavoritoAsync(car);
            return RedirectToAction("Favoritos");
        }
        public async Task<IActionResult> Favoritos()
        {
            List<Coche> coches = await this.service.GetCochesFavoritosAsync();
            return View(coches);
        }
        public async Task<IActionResult> DeleteFavorito(int idcoche)
        {
            await this.service.DeleteCocheFavoritoAsync(idcoche);
            return RedirectToAction("Favoritos");
        }
    }
}
