using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lost_Videogames.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lost_Videogames.Pages
{
    public class GameCreateModel : PageModel
    {
        public string errorMessage = ""; //Variável para apresentação de erros na pagina .cshtm
        public string successMessage = ""; //Variável para apresentação de success na pagina .cshtm

        public List<Publisher> PublishersEnabled = new List<Publisher>(); //Lista de Publishers
        public void OnGet()
        {
            LostGamesContext context = new LostGamesContext();//Context ligação entre o .Net e base de dados MySQL

            //Pesquisa de publishers no estado enabled
            PublishersEnabled = context.SearchPublishers("state", "enabled");

        }
        public void OnPost()
        {
            //Verifica que se todos os campos do formulário estão preenchidos,
            //mensagem de erro no caso de faltar algum campo
            if (
                Request.Form["selectpublisher"] == "" ||
                Request.Form["img_url"] == "" ||
                Request.Form["name"] == "" ||
                Request.Form["genre"] == "" ||
                Request.Form["patform"] == "" ||
                Request.Form["release_year"] == "" ||
                Request.Form["state"] == "")
            {
                errorMessage = "All fields are required";
                OnGet();
                return;

            }
            LostGamesContext context = new LostGamesContext();
            try
            {
                //Cria objeto Game com os dados do formulário
                //e faz Insert dos novos dados na base de dados

                Game game = new Game();
        
                game.id_publisher = Int32.Parse(Request.Form["selectpublisher"]);
                game.img_url = Request.Form["img_url"];
                game.name = Request.Form["name"];
                game.genre = Request.Form["genre"];
                game.platform = Request.Form["platform"];
                game.release_year = Int32.Parse(Request.Form["release_year"]);
                game.state = Request.Form["state"];
                
                context.CreateGames(game); //Game Insert na base de dados
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                OnGet();
                return;
            }

            successMessage = "Game created successfully"; //Apresenta mensagem de sucesso no caso do try = true
            OnGet();
        }
    }
}
