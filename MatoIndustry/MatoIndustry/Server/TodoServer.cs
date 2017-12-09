using System.Collections.Generic;
using System.Threading.Tasks;
using MatoIndustry.Helper;
using MatoIndustry.Model;
using Newtonsoft.Json;

namespace MatoIndustry.Server
{
    public class TodoServer
    {


        private static TodoServer instance;

        private TodoServer()
        {
        }

        public static TodoServer Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new TodoServer();
                }
                return instance;
            }
        }

        public async Task<List<TodoItemInfo>> GetTodoList()
        {
            List<TodoItemInfo> respose;
            var filePath = "TodoList.json";
            string text = await FileHelper.ReadAllTextAsync(filePath);
            if (!string.IsNullOrEmpty(text))
            {
                respose = JsonConvert.DeserializeObject<List<TodoItemInfo>>(text);

            }
            else
            {
                respose = new List<TodoItemInfo>();
            }
            return respose;
        }

        public async Task SaveTodoList(List<TodoItemInfo> todoList)
        {

            string filePath = "TodoList.json";

            string jsonContent = JsonConvert.SerializeObject(todoList);
            await FileHelper.WriteTextAllAsync(filePath, jsonContent);

        }
    }
}