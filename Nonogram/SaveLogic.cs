using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram
{
    public struct SaveLogicStruct_t
    {
        public string PuzzleTitle;
        public string Difficulty;
        public int SizeX;
        public int SizeY;
        public List<bool> Solution;
    }

    public class SaveLogic
    {
        public static SaveLogicStruct_t SaveLogicStruct;

        public static void RetrieveInformation(string PuzzleTitle,
                                               string Difficulty,
                                               int SizeX,
                                               int SizeY,
                                               List<bool> Solution)
        {
            SaveLogicStruct.PuzzleTitle = PuzzleTitle;
            SaveLogicStruct.Difficulty = Difficulty;
            SaveLogicStruct.SizeX = SizeX;
            SaveLogicStruct.SizeY = SizeY;
            SaveLogicStruct.Solution = Solution;
        }
    }
}
