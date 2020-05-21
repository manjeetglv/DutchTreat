using System.Collections.Generic;
using Drums.Data.Entities;

namespace Drums.Data.Repositories
{
    public interface IReportCardRepository
    {
        int AddReportCard(ReportCard reportCard);
        ReportCard GetReportCardById(int reportCardId);
    }
}