using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Initializer
{
    public class UsersInitializer
    {
        public static User[] GetUsers()
        {
            User[] users = new User[]
            {
                new User() {FirstName = "Pesho", LastName = "Peshev", Email = "pesho@abv.bg", Password = "pesho1234"},
                new User() {FirstName = "Gosho", LastName = "Goshev", Email = "gosho@abv.bg", Password = "gosho1234"},
                new User() {FirstName = "Kiro", LastName = "Kirov", Email = "kireto@abv.bg", Password = "kir4o99"},
                new User() {FirstName = "Ivan", LastName = "Ivanov", Email = "ivanovski@abv.bg", Password = "ivanovisch"},
                new User() {FirstName = "Marto", LastName = "Martinov", Email = "martin@abv.bg", Password = "fsdfsadf"},
                new User() {FirstName = "Aleks", LastName = "Aleksiev", Email = "alex@abv.bg", Password = "alekz5435"},
                new User() {FirstName = "Stoyan", LastName = "Stoyanov", Email = "stoyan@abv.bg", Password = "asdasd"},
                new User() {FirstName = "Rosen", LastName = "Rosev", Email = "rosen@abv.bg", Password = "rosen12354"},
                new User() {FirstName = "Niki", LastName = "Nikov", Email = "niki@abv.bg", Password = "niki1234"},
                new User() {FirstName = "Miro", LastName = "Mirov", Email = "miro@abv.bg", Password = "miro12354"},
                new User() {FirstName = "Vladi", LastName = "Vladimirov", Email = "vladi@abv.bg", Password = "vladi1234"},
                new User() {FirstName = "Hristo", LastName = "Hristo", Email = "hristo@abv.bg", Password = "hristo1234"},
                new User() {FirstName = "Ivo", LastName = "ivov", Email = "ivo@abv.bg", Password = "ivo1234"},

            };
            return users;
        }
    }
}