using System.ComponentModel;
using System.Windows.Input;
using MauiAppFits.Models;

namespace MauiAppFits.ViewModels
{
    [QueryProperty("PegarIdDaNavegacao", "parametro_id")]
    public class CadastroAtividadeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string descricao, observacoes;
        int id;
        DateTime data;
        double? peso;


        public string PegarIdDaNavegacao
        {
            set
            {
                int id_parametro = Convert.ToInt32(Uri.UnescapeDataString(value));
                VerAtividade.Execute(id_parametro);
            }
        }

        public string Descricao
        {
            get => descricao;
            set
            {
                descricao = value;
                PropertyChanged(this, new PropertyChangedEventArgs("descricao"));
            }
        }

        public string Observacoes
        {
            get => observacoes;
            set
            {
                observacoes = value;
                PropertyChanged(this, new PropertyChangedEventArgs("observacoes"));
            }
        }

        public int Id
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Id"));

            }
        }

        public DateTime Data
        {
            get => data;
            set
            {
                data = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Data"));
            }
        }

        public double? Peso
        {
            get => peso;
            set
            {
                peso = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Peso"));
            }
        }

        public ICommand NovaAtividade
        {
            get => new Command(() =>
            {
                Id = 0;
                Descricao = string.Empty;
                Data = DateTime.Now;
                Peso = null;
                Observacoes = string.Empty;
            });
        }

        public ICommand SalvarAtividade
        {
            get => new Command(async () =>
            {
                try
                {
                    Atividade model = new()
                    {
                        Descricao = this.Descricao,
                        Data = this.Data,
                        Peso = this.Peso,
                        Observacoes = this.Observacoes

                    };

                    if (this.Id == 0)
                    {
                        await App.DataBase.Insert(model);
                    }
                    else
                    {
                        model.Id = this.Id;
                        await App.DataBase.Update(model);
                    }

                    await Shell.Current.DisplayAlert("Beleza!", "Atividade Salva!", "OK");

                    await Shell.Current.GoToAsync("//MinhasAtividades");
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Ops", ex.Message, "OK");
                }
            });
        }

        public ICommand VerAtividade
        {
            get => new Command<int>(async (int id) =>
            {
                try
                {
                    Atividade model = await App.DataBase.GetById(id);

                    this.Id = model.Id;
                    this.Descricao = model.Descricao;
                    this.Peso = model.Peso;
                    this.Data = model.Data;
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Ops", ex.Message, "OK");
                }
            });
        }
    }
}
