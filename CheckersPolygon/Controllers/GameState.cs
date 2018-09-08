using CheckersPolygon.GameObjects;
using CheckersPolygon.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using CheckersPolygon.Helpers;

namespace CheckersPolygon.Controllers
{
    /* Состояние игры
     * Хранит в себе все динамические игровые объекты
     */
    [Serializable]
    class GameState
    {
        public List<BaseChecker> whiteCheckers = new List<BaseChecker>(12); // Белые шашки
        public List<BaseChecker> blackCheckers = new List<BaseChecker>(12); // Черные шашки
        public bool phase; // Фаза игры (определяет: кто вверху, кто внизу)
        public CheckerSide turn = CheckerSide.White; // Кому из игроков принадлежит право текущего хода
        public BaseChecker activeChecker = null; // Шашка в процессе хода
        public PathPoint activeCheckerPathPoint = null; // Карта ходов активной шашки
        public bool isAiControlled = false;
        public CheckerSide aiSide = CheckerSide.Black;
    }
}
