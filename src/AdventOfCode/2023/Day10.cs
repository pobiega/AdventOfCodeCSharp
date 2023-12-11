using AdventOfCode.Helpers;

using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public sealed class Day10 : AdventBase
{
    protected override object InternalPart1()
    {
        var map = Input.ToCharMap();

        var start = map.Find(x => x == 'S')
                    ?? throw new Exception("No starting position found");

        var startConnections = FindConnections(map, start).ToArray();

        var paths = startConnections
            .Select(sc => WalkMaze(map, start, sc))
            .ToArray();

        return paths[0].Keys.Select(loc => paths.Select(x => x[loc]).Min()).Prepend(0).Max();
    }

    private Dictionary<Point2d, int> WalkMaze(char[][] map, Point2d start, Point2d sc)
    {
        var distance = 1;
        var direction = FindDirection(start, sc);

        var currentPosition = sc;

        var dict = new Dictionary<Point2d, int> { { currentPosition, 1 } };

        while (currentPosition != start)
        {
            var nextPosition = GetNextPosition(map, currentPosition, direction);
            distance++;
            direction = FindDirection(currentPosition, nextPosition);
            currentPosition = nextPosition;

            if (currentPosition == start)
            {
                break;
            }

            dict.Add(currentPosition, distance);
        }

        return dict;
    }

    private Point2d GetNextPosition(char[][] map, Point2d currentPosition, Direction direction)
    {
        if (!map.TryGetValue(currentPosition, out var value))
        {
            throw new Exception("Can't move that way");
        }

        return value switch
        {
            '|' when direction == Direction.Down => new Point2d(currentPosition.Y + 1, currentPosition.X),
            '|' when direction == Direction.Up => new Point2d(currentPosition.Y - 1, currentPosition.X),

            '-' when direction == Direction.Right => new Point2d(currentPosition.Y, currentPosition.X + 1),
            '-' when direction == Direction.Left => new Point2d(currentPosition.Y, currentPosition.X - 1),

            'L' when direction == Direction.Left => new Point2d(currentPosition.Y - 1, currentPosition.X),
            'L' when direction == Direction.Down => new Point2d(currentPosition.Y, currentPosition.X + 1),

            'J' when direction == Direction.Right => new Point2d(currentPosition.Y - 1, currentPosition.X),
            'J' when direction == Direction.Down => new Point2d(currentPosition.Y, currentPosition.X - 1),

            '7' when direction == Direction.Right => new Point2d(currentPosition.Y + 1, currentPosition.X),
            '7' when direction == Direction.Up => new Point2d(currentPosition.Y, currentPosition.X - 1),

            'F' when direction == Direction.Left => new Point2d(currentPosition.Y + 1, currentPosition.X),
            'F' when direction == Direction.Up => new Point2d(currentPosition.Y, currentPosition.X + 1),

            _ => throw new Exception("Invalid move")
        };
    }

    private Direction FindDirection(Point2d start, Point2d sc)
    {
        var diff = sc - start;

        if (diff.X == 1)
            return Direction.Right;

        if (diff.X == -1)
            return Direction.Left;

        if (diff.Y == 1)
            return Direction.Down;

        if (diff.Y == -1)
            return Direction.Up;

        throw new Exception("Not a cardinal move");
    }

    private static IEnumerable<Point2d> FindConnections(char[][] map, Point2d s)
    {
        char value;

        if (map.TryGetValue(s.Y, s.X + 1, out value)
            && value is '-' or 'J' or '7')
            yield return new Point2d(s.Y, s.X + 1);

        if (map.TryGetValue(s.Y, s.X - 1, out value)
            && value is '-' or 'F' or 'L')
            yield return new Point2d(s.Y, s.X - 1);

        if (map.TryGetValue(s.Y + 1, s.X, out value)
            && value is '|' or 'J' or 'L')
            yield return new Point2d(s.Y + 1, s.X);

        if (map.TryGetValue(s.Y - 1, s.X, out value)
            && value is '|' or 'F' or '7')
            yield return new Point2d(s.Y - 1, s.X);
    }

    protected override object InternalPart2()
    {
        var map = Input.ToCharMap();

        var start = map.Find(x => x == 'S')
                    ?? throw new Exception("No starting position found");

        var startConnections = FindConnections(map, start).ToArray();

        var paths = startConnections
            .Select(sc => WalkMaze(map, start, sc))
            .ToArray();

        // find inner area somehow

        return 1;
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}