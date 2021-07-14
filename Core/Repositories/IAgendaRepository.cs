using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public interface IAgendaRepository
    {
        int CreateAgenda(Agenda agenda);

        void DeleteAgenda(Agenda agenda);

        List<Agenda> GetAgendas(int accountID);

        int GetWorkAgendaID(int accountID);
    }
}