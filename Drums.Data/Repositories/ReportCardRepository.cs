using System.Linq;
using Drums.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Drums.Data.Repositories
{
    public class ReportCardRepository: IReportCardRepository
    {
        private readonly DrumsDbContext _drumsDbContext;

        public ReportCardRepository(DrumsDbContext drumsDbContext)
        {
            _drumsDbContext = drumsDbContext;
        }
        public int AddReportCard(ReportCard reportCard)
        {
            _drumsDbContext.ReportCards.Add(reportCard);
            return _drumsDbContext.SaveChanges();
        }

        public ReportCard GetReportCardById(int reportCardId)
        {
            return _drumsDbContext.ReportCards.Include(reportCard => reportCard.LayoutSettings)
                .Include(reportCard => reportCard.ReportCardContentSettings)
                .Include(reportCard => reportCard.GradeSpecificSettings)
                .FirstOrDefault(reportCard => reportCard.Id == reportCardId);
        }
    }
}