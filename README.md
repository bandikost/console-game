<b>Описание проекта:</b><br>
RPG-битва с инвентарем и системой прогрессии.
- Этот проект реализует консольную игру в жанре RPG, где игрок управляет персонажем, сражается с монстрами, собирает опыт и золото, а также улучшает свои характеристики и инвентарь.
- В проекте представлены механики прокачки персонажа, покупки и продажи предметов, а также симуляции пошаговых сражений.
- Все события сопровождаются текстовыми описаниями, создающими атмосферу приключений в фантазийном мире.

<b>Основные возможности:</b>
<li>Персонажи: Есть три различных персонажа: Огр, Незнакомец и Гоблин. Каждый из них имеет уникальные характеристики (уровень, здоровье, опыт, золото и урон).</li>
<li>Система сражений: Пошаговые бои между персонажами с динамическим отображением урона, уровня здоровья и опыта.</li>
<li>Прокачка уровня: Персонажи могут накапливать опыт за победы в сражениях, переходить на новые уровни и получать улучшения характеристик.</li>
<li>Магазин предметов: Игрок может покупать оружие, которое увеличивает урон, и управлять своим инвентарем.</li>
<li>Инвентарь и продажа предметов: Возможность отслеживать купленные предметы и продавать их за половину стоимости.</li>
<li>Динамическое повествование: Тексты с диалогами и описаниями добавляют игровую атмосферу.</li>
<br>
<b>Использование:</b><br>
<li> Старт игры:</li>
  При запуске игры вы погружаетесь в пещеры, где ваши персонажи вступают в диалоги, а затем начинается серия сражений.</br>
<li> Сражения:</li>
  В бою персонажи обмениваются ударами до тех пор, пока здоровье одного из них не упадет до нуля. За каждую победу персонаж получает опыт и золото, которые можно использовать для улучшения характеристик.<br>
<li> Инвентарь и магазин:</li>
  В игре реализован магазин, где игрок может покупать различные виды оружия для улучшения урона своего персонажа. Можно также продавать предметы за золото.<br>

<b>Пример использования:</b><br>
// Создаем персонажа "Незнакомца" и начинаем серию сражений
- Characters stranger = new Person2();
- Battle battle = new Battle(stranger, new Person1()); 
- await battle.Start();

<b>Требования:</b>
- .NET Core 5.0 и выше.
<b>Лицензия:</b>
- MIT License.
