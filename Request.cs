using PC_Service.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PC_Service
{

    public class Request
    {
        EntitiesMain entities = new EntitiesMain();

        public List<Client> GridClient() 
        {
            List<Client> Client = entities.Client.ToList();
            return Client;
        }

        public void WorkClient(Client client) 
        {
            client = client ?? new Client();

            if (client.ClientId == 0)
            {
                try
                {
                    entities.Client.Add(client);
                    entities.SaveChanges();
                    MessageBox.Show("Данные сохранены");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var error in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in error.ValidationErrors)
                        {
                            MessageBox.Show("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
              
            }
            else 
            {
                try // если идет редактирование пользователя, а не добавление 
                {
                    entities.Entry(client).State = EntityState.Modified;
                    entities.SaveChanges();
                    MessageBox.Show("Данные сохранены");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }



        }


    }
}
