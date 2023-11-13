namespace basicTextRpg
{
    class Player
    {
        // initiate vars
        public int healthPoints;
        public string name;
        public int level;
        public int experience;
        public int attackDamage;
        public int weaponAD;
        public int manaPoints;
        public List<string> abilities = new List<string>();

        // constructor
        public Player(string _name)
        {
            name = _name;
            abilities.Add("Slash");
            weaponAD = 5;
            level = 1;
            healthPoints = 30;
            experience = 0;
            attackDamage = 5 + weaponAD;
            manaPoints = 0;
        }

        public bool GainExp(int exp)
        {
            experience += exp;
            if (experience >= 10 * level)
            {
                level++;
                experience = 0;
                attackDamage += 5;
                healthPoints += 10;
                manaPoints += 10;
                return true;
            }
            return false;
        }
        public string CombatMenu()
        {
            string tempString = "Combat Options:\n";
            for (int i = 0; i < abilities.Count; i++)
            {
                tempString += $"{i + 1} - " + abilities[i] + "\n";
            }
            tempString += $"\nCurrent HP: {healthPoints}\tCurrent MP: {manaPoints}\n";
            return tempString;
        }

        public string StatsMenu()
        {
            return $"Name: {name}\nLevel: {level}\nHP: {healthPoints}\nMP: {manaPoints}\nAttack Damage: {attackDamage}\nExp: {experience}\n";
        }

        public int Slash()
        {
            return attackDamage;
        }

        public int Fireball()
        {
            manaPoints -= 5;
            int damage = attackDamage + level * 2;
            return damage;
        }
    }

    class Slime
    {
        public int healthPoints;
        public int attackDamage;
        public int expDrop;
        public int level;
        public string type;
        public List<string> abilities = new List<string>();

        public Slime(int _level, string _type)
        {
            level = _level;
            type = _type;
            abilities.Add("Splash");
            healthPoints = 0 + level * 10;
            attackDamage = 0 + level * 3;
            expDrop = 5 + level * 5;
        }
        public string StatsMenu()
        {
            return $"Type: {type}\nLevel: {level}\nHP: {healthPoints}\nAttack Damage: {attackDamage}\n";
        }

        public int Splash()
        {
            return attackDamage;
        }

    }

    class Program
    {
        static void SlowType(string myString)
        {
            foreach (char letter in myString)
            {
                Console.Write(letter);
                Thread.Sleep(15);
            }
        }

        static void NextText()
        {
            Console.ReadLine();
            Console.Clear();
        }

        static void Main(string[] args)
        {
            Console.Clear();
            SlowType("Welcome to the Cool Text-Based RPG Game!");
            SlowType("\nTo start, please press Enter");
            NextText();
            bool validEntry = false;
            string? readResult;
            string playerSelection;
            string playerName = "";
            int damage;
            SlowType("Give a name to your hero: ");
            do
            {
                readResult = Console.ReadLine();
                if (!string.IsNullOrEmpty(readResult))
                {
                    playerName = readResult;
                    validEntry = true;
                }
                else
                {
                    SlowType("Please input a name: ");
                }
            } while (!validEntry);

            Player player = new Player(playerName);
            Slime slime1 = new Slime(1, "Adorable");
            Slime slime2 = new Slime(2, "Daring");

            Console.Clear();
            SlowType($"Nice to meet you {player.name}! (press Enter to continue)");
            NextText();
            SlowType("Here are your starting stats:\n\n");
            SlowType(player.StatsMenu());
            NextText();
            SlowType("You can gain more stats as you progress thorugh the story!");
            NextText();
            SlowType($"Watch out! A level {slime1.level} {slime1.type} Slime is approaching you!");
            NextText();
            SlowType(slime1.StatsMenu());
            NextText();
            SlowType($"It is SO cute to attack but you need to kill it to continue.");
            NextText();
            SlowType(player.CombatMenu() + "\n");
            SlowType("To slash your sword press 1 (and Enter)\n");
            validEntry = false;
            do
            {
                readResult = Console.ReadLine();
                if (!string.IsNullOrEmpty(readResult))
                {
                    playerSelection = readResult;
                    if (playerSelection == "1")
                    {
                        Console.Clear();
                        SlowType($"You dealt {player.Slash()} damage and killed the Slime!");
                        if (player.GainExp(slime1.expDrop))
                        {
                            SlowType($"\nCongrats, you leveled up! You are now level {player.level}!");
                            NextText();
                            SlowType($"Your new stats:\n{player.StatsMenu()}");
                        }
                        validEntry = true;
                    }
                    else
                        SlowType($"{readResult} is not a valid option.\n");
                }
                else
                    SlowType("Please enter an option.\n");
            } while (!validEntry);
            NextText();
            SlowType($"You unlocked a new ability \"Fireball\"!");
            player.abilities.Add("Fireball");
            NextText();
            SlowType("It is added to your combat options. (Costs 5 MP to cast)\n");
            SlowType(player.CombatMenu());
            NextText();
            SlowType("You start walking along the path.");
            NextText();
            SlowType("...");
            NextText();
            SlowType("...");
            NextText();
            SlowType("'Why is it so quiet here?'");
            NextText();
            SlowType("...");
            NextText();
            SlowType($"There, a {slime2.type} Slime!");
            NextText();
            SlowType(slime2.StatsMenu());
            NextText();
            SlowType("Before you can react, it attacks!");
            NextText();
            SlowType($"The Slime used \"{slime2.abilities[0]}\"!");
            NextText();
            damage = slime2.Splash();
            player.healthPoints -= damage;
            SlowType($"It dealt {damage} damage. You now have {player.healthPoints} HP!");
            NextText();

            // combat loop
            do
            {
                // player input loop
                do
                {
                    validEntry = false;
                    SlowType(player.CombatMenu());
                    readResult = Console.ReadLine();
                    if (!string.IsNullOrEmpty(readResult))
                    {
                        playerSelection = readResult;

                        switch (playerSelection)
                        {
                            case "1":
                                {
                                    validEntry = true;
                                    Console.Clear();
                                    SlowType($"You used \"{player.abilities[0]}\".");
                                    damage = player.Slash();
                                    slime2.healthPoints -= damage;
                                    NextText();
                                    SlowType($"It dealt {damage} damage.");
                                    NextText();
                                    SlowType($"Slime HP: {slime2.healthPoints}");
                                    NextText();
                                    break;
                                }
                            case "2":
                                {
                                    if (player.manaPoints >= 5)
                                    {
                                        validEntry = true;
                                        Console.Clear();
                                        SlowType($"You used {player.abilities[1]}.");
                                        damage = player.Fireball();
                                        slime2.healthPoints -= damage;
                                        NextText();
                                        SlowType($"It dealt {player.Fireball()} damage. The Slime has {slime2.healthPoints} HP!");
                                        NextText();
                                        break;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        SlowType($"You need 5 MP to cast \"{player.abilities[1]}\".");
                                        NextText();
                                        break;
                                    }
                                }
                            default:
                                {
                                    Console.Clear();
                                    SlowType($"{readResult} is not a valid option.\n");
                                    break;
                                }

                        }
                    }
                    else
                    {
                        Console.Clear();
                        SlowType("Please enter an option.\n");
                    }
                } while (validEntry == false);

                if (slime2.healthPoints <= 0)
                    continue;

                SlowType($"The Slime used \"{slime2.abilities[0]}\"!");
                NextText();
                damage = slime2.Splash();
                player.healthPoints -= damage;
                SlowType($"It dealt {damage} damage. You now have {player.healthPoints} HP!");
                NextText();

            } while (slime2.healthPoints > 0 && player.healthPoints > 0);

            SlowType("You killed the Slime!"); // no logic for player dying yet (how can you even die?)
            NextText();
            SlowType(player.GainExp(slime2.expDrop) ? $"Congrats you are now level {player.level}!" : "");
            NextText();
        }
    }
}