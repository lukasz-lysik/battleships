using System;
using Battleships.Domain;
using Battleships.Domain.Builders;
using Battleships.Domain.RandomGenerators;

namespace Battleships.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            var gameBuilder = new GameBoardBuilder(
                new ShipPositionRandomGenerator(
                    new DirectionRandomGenerator(random),
                    new CoordinatesRandomGenerator(random)
                    )
                );

            var game = gameBuilder.Build(10, 10,
                new Ship(4),
                new Ship(4),
                new Ship(5)
            );

            while(true)
            {
                Console.Write("Hit: ");
                var input = Console.ReadLine();

                if (ExitRequested(input))
                    return;

                var hitCoordinates = Coordinates.FromString(input);
                if (!hitCoordinates.HasValue)
                {
                    Console.WriteLine("ERR Incorrect input: {0}", input);
                    continue;
                }

                var result = game.Hit(hitCoordinates.Value);
                Console.WriteLine("RESULT: {0}", result);
            }
        }

        private static bool ExitRequested(string input)
        {
            return string.IsNullOrEmpty(input);
        }
    }
}
