using System;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


public class Characters
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Level { get; set; }
    public int ExpDeathGiven { get; set; }
    public int CurrentExp { get; set; }
    public int CurrentGold{ get; set; }
    public int GoldDeathGiven { get; set; }
    public int BaseDamage { get; set; }
    public int Weapon { get; set; }
    public string Hit { get; set; }
    public int ExperienceSystem { get; set; }

    private static Random _random = new Random();

    public Characters()
    {
        Name = "";
        Health = 100;
        Level = 1;
        CurrentExp = 0;
        ExpDeathGiven = _random.Next(49, 71);
        GoldDeathGiven = _random.Next(30, 38);
        BaseDamage = 6;
        Hit = "ударил";
    }

    public void GainExperience(int exp)
    {
        ExperienceSystem = 100 + (50 * (Level - 1));
        CurrentExp += exp;
        while (CurrentExp >= ExperienceSystem)
        {
            LevelUp();
            
        }
    }

    

    public void GainGold(int gold)
    {
        CurrentGold += gold;
    }

    protected virtual void LevelUp()
    {
        Level++;
        CurrentExp -= 100;
        Health += Level * 100 / 2;

    }
}

public class Person1 : Characters
{

    public Person1()
    {
        Name = "Огр";
  
     }
}
public class Person2 : Characters
{
    public Person2()
    {
        Name = "Незнакомец";
        Health = 100;
        Level = 1;
        ExpDeathGiven = 100;
        CurrentExp = 0;
        CurrentGold = 0;
        BaseDamage = 15;   
    }

    protected override void LevelUp()
    {
        Level++;
        CurrentExp -= 100;
        Health += Level * 100 / 2;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("");
        Console.WriteLine($" {Name} достиг {Level} уровня!");
        Console.ResetColor();
        Console.WriteLine($" Ваше текущее Здоровье: {Health}");
        Console.WriteLine($" Ваш текущий Опыт: {CurrentExp}");
        Console.WriteLine($" Ваше текущее Золотишко: {CurrentGold}");
    }

    public void Hits()
    {      
        if ( Level > 0 )
        {

        }
    }
}
public class Person3 : Characters
{
    public Person3()
    {
        Name = "Гоблин";
    
    }
}
public class ItemManager
{
    private List<(int Index, string Key, int Damage, int Gold, string UnderClass)> items;
    private Characters _hero;
    private (int Index, string Key, int Damage, int Gold, string UnderClass) _selectedItem;
    public int ItemsCounter { get; set; }
    public ItemManager(Characters hero)
    {   
        _hero = hero;
        _hero.Level = _hero.Level;
        items = new List<(int, string, int, int, string)>();
   
        
        items = new List<(int, string, int, int, string)>
        {
            (1, "Нож", 18, 9, "cold"),
            (2, "Рыба-нож", 2, 21, "cold"),
            (3, "Пистолет", 6, 29, "cold"),
            
        };
        
    }

    public void DisplayItems()
    {
        Console.WriteLine("");
        Console.WriteLine($"У меня тут завалялось пару вещей, может тебе все таки нужно другое оружие или ты дальше пойдешь с этим?");
        foreach (var item in items)
        {
            Console.Write($"{item.Index} | {item.Key} - {item.Damage} урона");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" {item.Gold} золота.");
            Console.ResetColor();
        }
    }


    public void SelectItem()
    {      
        while (true)
        {
            Console.WriteLine("");
            Console.WriteLine("0 - для выхода из лавки");
            Console.Write("Какой предмет тебя интересует? ");  
            string input = Console.ReadLine();
            if (int.TryParse(input, out int index) && (index >= 1 && index <= items.Count))
            {
                var selectedItem = items.Find(item => item.Index == index);
                var UpDamage = selectedItem.Damage;
                if (selectedItem.Gold !<= _hero.CurrentGold)
                {
                     
                    _hero.CurrentGold -= selectedItem.Gold;
                    items.Remove(selectedItem);
                    _hero.BaseDamage += UpDamage;
                    Console.Write($"После покупки {selectedItem.Key} у вас осталось");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($" {_hero.CurrentGold} золота.");
                    Console.ResetColor ();
                    Console.WriteLine($"Теперь ваш урон {_hero.BaseDamage}!");
                    Console.WriteLine("");   
                    _selectedItem = selectedItem; // Передать значение selectItem в _selectedItem, для создания инвентаря.
                    break;
                        
                }
                else
                {
                    var GoldLess = selectedItem.Gold -= _hero.CurrentGold;
                    Console.WriteLine($"У вас недостаточно {GoldLess} золота для покупки этого предмета.");
                }
                break;
            }

            if (input == "0")
            {
                break;
            }

            else
            {
                Console.WriteLine("Неверный ввод. Попробуйте снова.");
            }
        }
    }

    public void Inventory()
    {
        int ItemsCounter = 0;
        if (_selectedItem != default)
        {
            ItemsCounter++;
            Console.WriteLine($"В инвентаре находятся {ItemsCounter} предметы: {_selectedItem.Key}");

        }
        else
        {
            Console.WriteLine("Инвентарь пуст.");
        }
    }

    public void SellItems()
    {
        int salePrice;
        if (_selectedItem != default)
        {
                salePrice = _selectedItem.Gold / 2;
                Console.WriteLine($"Вы можете продать его за {salePrice} золота.");
                Console.Write("Будешь продавать? ");
                string input = Console.ReadLine();
                if (input == "да")
                {
                    items.Remove(_selectedItem);
                    _hero.CurrentGold += _selectedItem.Gold;
                    _hero.BaseDamage = _hero.BaseDamage - _selectedItem.Damage;
                    Console.Write("+ ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{salePrice} золота.");
                    Console.ResetColor();
                    Console.WriteLine($"Теперь ваш урон {_hero.BaseDamage}!");
            }
                


        }
    }
}
public class ResourceManager
{
    private Characters _character;

    public ResourceManager(Characters character)
    {
        _character = character;
    }

    public void RestoreLevel(Characters monster)
    {
        monster.Level = monster.Level + 1;     
    }

    public void RestoreHealth()
    {
        _character.Health = 100 * _character.Level; 
    }
}
public class Battle
{
    private readonly Characters _hero;
    private readonly Characters _monster;

    public Battle(Characters hero, Characters monster)
    {
        _hero = hero;
        _monster = monster;
    }

    public async Task Start()
    {
        ResourceManager healthManager = new ResourceManager(_monster);

        while (_hero.Health > 0 && _monster.Health > 0)
        {
            int heroDamage = _hero.BaseDamage;
            _monster.Health -= heroDamage;
            await Task.Delay(0);
            Console.WriteLine($"{_hero.Name} {_hero.Hit} на {heroDamage} урона {_monster.Name}. | {_monster.Name} осталось {_monster.Health} здоровья. |");

            if (_monster.Health <= 0)
            {

                _hero.GainGold(_monster.GoldDeathGiven);
                _hero.GainExperience(_monster.ExpDeathGiven);
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($" + {_monster.ExpDeathGiven} опыта за убийство {_monster.Name}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" + {_monster.GoldDeathGiven} золота за убийство {_monster.Name}");
                Console.ResetColor();
                Console.WriteLine("");
                healthManager.RestoreLevel(_monster);
                healthManager.RestoreHealth();            
                break;
            }

            int monsterDamage = _monster.BaseDamage;
            _hero.Health -= monsterDamage;
            await Task.Delay(0);
            Console.WriteLine($"{_monster.Name} ударил на {monsterDamage} урона {_hero.Name}. | {_hero.Name} осталось {_hero.Health} здоровья. |");

            if (_hero.Health <= 0)
            {
                Console.WriteLine($"{_hero.Name} погиб в битве. Игра окончена.");
                Environment.Exit(0);
            }

        }

    }
}

public class StartMessagesToPlay
{
    readonly Characters orc = new Person1();
    readonly Characters stranger = new Person2();
    readonly Characters goblin = new Person3();

    public async Task OpenEyesAsync()
    {
        Console.WriteLine("*Медленно открываете глаза*");
        await Task.Delay(200);
        Console.WriteLine($"{stranger.Name}: Что это еще за пещера, не припоминаю чтобы я любил по горам ползать...");
        await Task.Delay(200);
        Console.WriteLine($"{goblin.Name}: *Агграхахпю... Проснулся человечишка. Страшно, Морги, помогай*");
        await Task.Delay(200);
        Console.WriteLine($"{orc.Name}: *Давно пора бы проснуться, Ну и что мне делать с ним? Съесть что-ли...*");
        await Task.Delay(200);
        Console.WriteLine("");     
    }
    

}


class Program
{
    static async Task Main(string[] args)
    {
        Characters orc = new Person1();
        Characters stranger = new Person2();
        Characters goblin = new Person3();

        StartMessagesToPlay messages = new StartMessagesToPlay();
        ItemManager itemManager = new(stranger);

        await messages.OpenEyesAsync();

        Battle battle1 = new(stranger, orc);
        await battle1.Start();

        while (stranger.Health > 0)
        {

            Console.Write("Хотите начать следующую драку в этой локации? (да/нет): ");
#pragma warning disable CS8600
            string input = Console.ReadLine();
            Console.WriteLine("");
            if (input == "да")
            {
                Console.WriteLine("Вы решили начать следующую драку.");
                Console.WriteLine("");
                Battle battle2 = new Battle(stranger, goblin);
                await battle2.Start();
                break;
            }
            if (input == "нет")
            {
                Console.WriteLine("Вы не решили начать следующую драку.");
                break;
            }
            else
            {
                continue;
            }
        }

        // Лавка дедули
        Console.WriteLine("*Рассказчик* Дальше, путешествуя по подземному миру, ты встречаешь деда, который сходу урчит и говорит тебе: ");
        Console.WriteLine("*Кряхтящий дед* Я очень рад что в этом безумном месте есть еще хоть кто-то.");
        itemManager.DisplayItems();
        itemManager.SelectItem();

        Console.WriteLine("Тебя как зовут вообще?: ");
#pragma warning disable CS8601
        stranger.Name = Console.ReadLine();
        Console.WriteLine("");
        Console.WriteLine($"{stranger.Name}, Неплохое имя!");

        // Третья драка
        Console.WriteLine("");
        Battle battle3 = new(stranger, orc);
        await battle3.Start();


        itemManager.Inventory();
        itemManager.SellItems();
    }

}