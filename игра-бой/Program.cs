using System;
using System.Collections.Generic;

namespace EnhancedTextRPG
{
    // Базовый класс для всех предметов
    abstract class Item
    {
        public string Name { get; protected set; }
        public abstract void Use(Character character);
    }

    class HealthPotion : Item
    {
        public HealthPotion()
        {
            Name = "Зелье здоровья";
        }

        public override void Use(Character character)
        {
            character.Health = Math.Min(character.Health + 30, character.MaxHealth);
            Console.WriteLine($"{character.Name} использовал {Name} (+30 HP)!");
        }
    }

    class ManaPotion : Item
    {
        public ManaPotion()
        {
            Name = "Зелье маны";
        }

        public override void Use(Character character)
        {
            if (character is IManaUser manaUser)
            {
                manaUser.Mana += 20;
                Console.WriteLine($"{character.Name} использовал {Name} (+20 MP)!");
            }
        }
    }

    // Интерфейс для персонажей, использующих ману
    interface IManaUser
    {
        int Mana { get; set; }
    }

    // Интерфейс для персонажей, использующих стрелы
    interface IArrowUser
    {
        int Arrows { get; set; }
    }

    abstract class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; protected set; }
        public int Damage { get; protected set; }
        public int Experience { get; set; }
        public int Level { get; set; } = 1;
        public List<Item> Inventory { get; } = new List<Item>();

        public abstract void Attack(Character target);
        public abstract void Initialize();

        public void UseItem(int itemIndex)
        {
            if (itemIndex >= 0 && itemIndex < Inventory.Count)
            {
                Console.Clear();
                Inventory[itemIndex].Use(this);
                Inventory.RemoveAt(itemIndex);
            }
            else
            {
                Console.WriteLine("Неверный выбор предмета!");
            }
        }

        public bool IsAlive => Health > 0;

        public void LevelUp()
        {
            if (Experience >= Level * 100)
            {
                Experience -= Level * 100;
                Level++;
                MaxHealth += 10;
                Health = MaxHealth;
                Damage += 2;
                Console.WriteLine($"\n{Name} достиг {Level} уровня! +10 к максимальному здоровью, +2 к урону!");
            }
        }

        public void RestoreResources()
        {
            Health = MaxHealth;

            if (this is IArrowUser archer)
                archer.Arrows = 10;

            if (this is IManaUser mage)
                mage.Mana = 50;
        }
    }

    class Archer : Character, IArrowUser
    {
        public int Arrows { get; set; }

        public override void Initialize()
        {
            Name = "Стрелок";
            Health = MaxHealth = 80;
            Damage = 15;
            Arrows = 10;
            Inventory.AddRange(new Item[] { new HealthPotion(), new HealthPotion() });
        }

        public override void Attack(Character target)
        {
            if (Arrows > 0)
            {
                Console.WriteLine($"{Name} стреляет в {target.Name} и наносит {Damage} урона!");
                target.Health -= Damage;
                Arrows--;
                Console.WriteLine($"Осталось стрел: {Arrows}");
            }
            else
            {
                Console.WriteLine($"{Name} пытается стрелять, но стрелы закончились!");
            }
        }
    }

    class Mage : Character, IManaUser
    {
        public int Mana { get; set; }

        public override void Initialize()
        {
            Name = "Маг";
            Health = MaxHealth = 60;
            Damage = 25;
            Mana = 50;
            Inventory.AddRange(new Item[] { new HealthPotion(), new ManaPotion() });
        }
        public override void Attack(Character target)
        {
            if (Mana >= 10)
            {
                Console.WriteLine($"{Name} бросает огненный шар в {target.Name} и наносит {Damage} урона!");
                target.Health -= Damage;
                Mana -= 10;
                Console.WriteLine($"Осталось маны: {Mana}");
            }
            else
            {
                Console.WriteLine($"{Name} пытается атаковать, но не хватает маны!");
            }
        }
    }

    class Warrior : Character
    {
        public override void Initialize()
        {
            Name = "Воин";
            Health = MaxHealth = 100;
            Damage = 20;
            Inventory.AddRange(new Item[] { new HealthPotion(), new HealthPotion() });
        }

        public override void Attack(Character target)
        {
            Console.WriteLine($"{Name} бьёт мечом {target.Name} и наносит {Damage} урона!");
            target.Health -= Damage;
        }
    }

    class Game
    {
        private Character player;
        private Random random = new Random();

        public void Start()
        {
            Console.WriteLine("Улучшенная текстовая RPG");
            player = CreateCharacter();
            PlayGame();
            Console.WriteLine($"\nИгра завершена. Ваш персонаж достиг {player.Level} уровня.");
        }

        private Character CreateCharacter()
        {
            Console.WriteLine("Выберите класс персонажа:");
            Console.WriteLine("1. Стрелок (80 HP, 15 урона, стрелы)");
            Console.WriteLine("2. Маг (60 HP, 25 урона, мана)");
            Console.WriteLine("3. Воин (100 HP, 20 урона)");

            while (true)
            {
                Console.Write("Ваш выбор (1-3): ");
                var input = Console.ReadLine();

                Character character = input switch
                {
                    "1" => new Archer(),
                    "2" => new Mage(),
                    "3" => new Warrior(),
                    _ => null
                };

                if (character != null)
                {
                    character.Initialize();
                    return character;
                }

                Console.WriteLine("Некорректный ввод!");
            }
        }

        private Character CreateEnemy()
        {
            Character enemy = random.Next(1, 4) switch
            {
                1 => new Archer() { Name = "Вражеский стрелок" },
                2 => new Mage() { Name = "Вражеский маг" },
                3 => new Warrior() { Name = "Вражеский воин" },
                _ => new Warrior() { Name = "Враг" }
            };

            enemy.Initialize();
            return enemy;
        }

        private void ShowBattleStatus(Character player, Character enemy)
        {
            Console.WriteLine($"\n{player.Name} (Ур.{player.Level}): {player.Health}/{player.MaxHealth} HP");
            Console.WriteLine($"{enemy.Name}: {enemy.Health}/{enemy.MaxHealth} HP");

            if (player is IArrowUser archer)
                Console.WriteLine($"Стрелы: {archer.Arrows}");
            if (player is IManaUser mage)
                Console.WriteLine($"Мана: {mage.Mana}");
        }

        private void PlayerTurn(Character player, Character enemy)
        {
            Console.WriteLine("\nВаш ход:");
            Console.WriteLine("1. Атаковать");
            Console.WriteLine("2. Использовать предмет");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();
            Console.Clear();

            if (choice == "1")
            {
                player.Attack(enemy);
            }
            else if (choice == "2")
            {
                ShowInventory(player);
            }
            else
            {
                Console.WriteLine("Пропуск хода из-за неверного ввода!");
            }
        }
        private void ShowInventory(Character player)
        {
            Console.WriteLine("Инвентарь:");
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.Inventory[i].Name}");
            }

            Console.Write("Выберите предмет (или 0 для отмены): ");
            if (int.TryParse(Console.ReadLine(), out int itemIndex) && itemIndex > 0)
            {
                player.UseItem(itemIndex - 1);
            }
        }

        private void EnemyTurn(Character enemy, Character player)
        {
            Console.WriteLine($"\nХод {enemy.Name}...");
            if (random.Next(0, 4) == 0 && enemy.Inventory.Count > 0)
            {
                enemy.UseItem(0);
            }
            else
            {
                enemy.Attack(player);
            }
        }

        private void PlayGame()
        {
            bool playing = true;
            while (playing)
            {
                var enemy = CreateEnemy();
                Console.WriteLine($"\nПоявился {enemy.Name}! Начинается битва!");

                while (player.IsAlive && enemy.IsAlive)
                {
                    ShowBattleStatus(player, enemy);
                    PlayerTurn(player, enemy);
                    if (enemy.IsAlive) EnemyTurn(enemy, player);
                }

                if (player.IsAlive)
                {
                    HandleVictory();
                    playing = AskToContinue();
                }
                else
                {
                    Console.WriteLine("\nВы проиграли! Игра окончена.");
                    playing = false;
                }
            }
        }

        private void HandleVictory()
        {
            int expGain = 50 + random.Next(0, 30);
            player.Experience += expGain;
            Console.WriteLine($"\nВы победили! Получено {expGain} опыта.");
            player.LevelUp();
            player.RestoreResources();
            Console.WriteLine("\nПосле боя вы восстановили здоровье и ресурсы.");
        }

        private bool AskToContinue()
        {
            Console.Write("\nХотите продолжить сражения? (да/нет): ");
            return Console.ReadLine().ToLower() == "да";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new Game().Start();
        }
    }
}