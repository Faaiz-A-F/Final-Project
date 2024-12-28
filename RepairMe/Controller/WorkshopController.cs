using RepairMe.Model.Entity;
using RepairMe.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using RepairMe.Model.Context;


namespace RepairMe.Controller
{
    internal class WorkshopController
    {
        private readonly WorkshopRepository _workshopRepository;

        // Default constructor
        public WorkshopController(DbContext context)
        {
            _workshopRepository = new WorkshopRepository(context);
        }

        // Constructor for dependency injection
        public WorkshopController(WorkshopRepository workshopRepository)
        {
            _workshopRepository = workshopRepository;
        }

        public List<Workshop> SearchWorkshops(string keyword)
        {
            return _workshopRepository.SearchWorkshops(keyword);
        }

        public Workshop GetBestWorkshop()
        {
            return _workshopRepository.GetBestWorkshop();
        }
    }
}
