using ClinicaVeterinaraMobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaVeterinaraMobile.Data
{
    public class ClinicaDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ClinicaDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Appointment>().Wait();
            _database.CreateTableAsync<Vet>().Wait();
        }

        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public Task<User> LoginAsync(string username, string password)
        {
            return _database.Table<User>()
                            .Where(i => i.Username == username && i.Password == password)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsAsync(int userId)
        {
            var appointments = await _database.Table<Appointment>()
                                            .Where(a => a.UserId == userId)
                                            .ToListAsync();

            var vets = await _database.Table<Vet>().ToListAsync();

            foreach (var app in appointments)
            {
                var mediculGasit = vets.FirstOrDefault(v => v.ID == app.VetID);

                if (mediculGasit != null)
                {
                    app.VetDetails = mediculGasit.VetName + " - " + app.Motiv;
                }
                else
                {
                    app.VetDetails = "Fără medic alocat - " + app.Motiv;
                }
            }

            return appointments;
        }

        public Task<int> SaveAppointmentAsync(Appointment appointment)
        {
            if (appointment.ID != 0)
            {
                return _database.UpdateAsync(appointment);
            }
            else
            {
                return _database.InsertAsync(appointment);
            }
        }

        public Task<int> DeleteAppointmentAsync(Appointment appointment)
        {
            return _database.DeleteAsync(appointment);
        }

        public Task<List<Vet>> GetVetsAsync()
        {
            return _database.Table<Vet>().ToListAsync();
        }

        public Task<int> SaveVetAsync(Vet vet)
        {
            if (vet.ID != 0) return _database.UpdateAsync(vet);
            else return _database.InsertAsync(vet);
        }

        public Task<int> DeleteVetAsync(Vet vet)
        {
            return _database.DeleteAsync(vet);
        }
    }
}