using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Treasure_
{
    class GamePole
    {
        private static Random rnd = new Random();
        public static void Generate(Game1 game)
        {
            Vector2[] pos = new Vector2[Game1.swampSize];
            Vector2 tileNow = new Vector2();
            int rnd1;
            do
            {
                game.isMapGood = true;
                for (int i = 0; i < Game1.width; i++)
                {
                    for (int f = 0; f < Game1.height; f++)
                    {
                        game.tiles[i, f] = new Tile();
                    }
                }
                game.portals = new Tile[3];
                int swampCount = (2 + (rnd.Next() % 3));
                tileNow = new Vector2(rnd.Next() % Game1.width, 0);
                game.tiles[(int)tileNow.X, (int)tileNow.Y].type = 1;
                //river
                while (true)
                {
                    rnd1 = rnd.Next() % 3;
                    switch (rnd1)
                    {
                        case 0:
                            if ((tileNow.X != 0) && !((tileNow.Y != 0) && (game.tiles[(int)tileNow.X - 1, (int)tileNow.Y - 1].type == 1)) && (game.tiles[(int)tileNow.X - 1, (int)tileNow.Y].type == 2))
                            {
                                game.tiles[(int)tileNow.X - 1, (int)tileNow.Y].type = 1;
                                game.tiles[(int)tileNow.X - 1, (int)tileNow.Y].position = new Vector2((int)tileNow.X - 1, (int)tileNow.Y);
                                game.tiles[(int)tileNow.X, (int)tileNow.Y].nextRiver = game.tiles[(int)tileNow.X - 1, (int)tileNow.Y];
                                tileNow.X = (int)tileNow.X - 1;
                            }
                            break;
                        case 1:
                            if (tileNow.Y != (Game1.height - 1))
                            {
                                game.tiles[(int)tileNow.X, (int)tileNow.Y + 1].type = 1;
                                game.tiles[(int)tileNow.X, (int)tileNow.Y + 1].position = new Vector2((int)tileNow.X, (int)tileNow.Y + 1);
                                game.tiles[(int)tileNow.X, (int)tileNow.Y].nextRiver = game.tiles[(int)tileNow.X, (int)tileNow.Y + 1];
                                tileNow.Y = (int)tileNow.Y + 1;
                            }
                            else
                            {
                                //решётка
                                game.lastRiverTile.position = new Vector2(tileNow.X, tileNow.Y);
                                goto Break1;
                            }
                            break;
                        case 2:
                            if ((tileNow.X != Game1.width - 1) && !((tileNow.Y != 0) && (game.tiles[(int)tileNow.X + 1, (int)tileNow.Y - 1].type == 1)) && (game.tiles[(int)tileNow.X + 1, (int)tileNow.Y].type == 2))
                            {
                                game.tiles[(int)tileNow.X + 1, (int)tileNow.Y].type = 1;
                                game.tiles[(int)tileNow.X + 1, (int)tileNow.Y].position = new Vector2((int)tileNow.X + 1, (int)tileNow.Y);
                                game.tiles[(int)tileNow.X, (int)tileNow.Y].nextRiver = game.tiles[(int)tileNow.X + 1, (int)tileNow.Y];
                                tileNow.X = (int)tileNow.X + 1;
                            }
                            break;
                    }
                }
                Break1:;
                //////////////////////болото
                ///
                for (int s = 0; s < swampCount; s++)
                {
                    for (int r = 0; r < 100; r++)
                    {
                        do
                        {
                            pos[0] = new Vector2(rnd.Next() % Game1.width, rnd.Next() % Game1.height);
                        }
                        while ((game.tiles[(int)pos[0].X, (int)pos[0].Y].type != 2)
                        || ((pos[0].X != 0) && game.tiles[(int)pos[0].X - 1, (int)pos[0].Y].type == 0)
                        || ((pos[0].X != (Game1.width - 1)) && game.tiles[(int)pos[0].X + 1, (int)pos[0].Y].type == 0)
                        || ((pos[0].Y != 0) && game.tiles[(int)pos[0].X, (int)pos[0].Y - 1].type == 0)
                        || ((pos[0].Y != (Game1.height - 1)) && game.tiles[(int)pos[0].X, (int)pos[0].Y + 1].type == 0));
                        for (int i = 0; i < Game1.swampSize - 1; i++)
                        {
                            rnd1 = rnd.Next() % 4;
                            pos[i + 1] = pos[i];
                            for (int f = 0; f < 4; f++)
                            {
                                switch (rnd1)
                                {
                                    case 0:
                                        if ((pos[i].Y != 0)
                                        && (game.tiles[(int)pos[i].X, (int)pos[i].Y - 1].type == 2)
                                        && ((pos[i].Y == 1) || (game.tiles[(int)pos[i].X, (int)pos[i].Y - 2].type != 0))
                                        && ((pos[i].X == Game1.width - 1) || (game.tiles[(int)pos[i].X + 1, (int)pos[i].Y - 1].type != 0))
                                        && ((pos[i].X == 0) || (game.tiles[(int)pos[i].X - 1, (int)pos[i].Y - 1].type != 0)))
                                        {
                                            for (int k = 0; k < i; k++)
                                            {
                                                if (pos[k] == new Vector2(pos[i].X, pos[i].Y - 1))
                                                    goto Break3;
                                            }
                                            pos[i + 1].Y--;
                                            goto Break2;
                                        }
                                        break;
                                    case 1:
                                        if ((pos[i].X != 0)
                                        && (game.tiles[(int)pos[i].X - 1, (int)pos[i].Y].type == 2)
                                        && ((pos[i].X == 1) || (game.tiles[(int)pos[i].X - 2, (int)pos[i].Y].type != 0))
                                        && ((pos[i].Y == Game1.height - 1) || (game.tiles[(int)pos[i].X - 1, (int)pos[i].Y + 1].type != 0))
                                        && ((pos[i].Y == 0) || (game.tiles[(int)pos[i].X - 1, (int)pos[i].Y - 1].type != 0)))
                                        {
                                            for (int k = 0; k < i; k++)
                                            {
                                                if (pos[k] == new Vector2(pos[i].X - 1, pos[i].Y))
                                                    goto Break3;
                                            }
                                            pos[i + 1].X--;
                                            goto Break2;
                                        }
                                        break;
                                    case 2:
                                        if ((pos[i].Y != Game1.height - 1)
                                        && (game.tiles[(int)pos[i].X, (int)pos[i].Y + 1].type == 2)
                                        && ((pos[i].Y == Game1.height - 2) || (game.tiles[(int)pos[i].X, (int)pos[i].Y + 2].type != 0))
                                        && ((pos[i].X == Game1.width - 1) || (game.tiles[(int)pos[i].X + 1, (int)pos[i].Y + 1].type != 0))
                                        && ((pos[i].X == 0) || (game.tiles[(int)pos[i].X - 1, (int)pos[i].Y + 1].type != 0)))
                                        {
                                            for (int k = 0; k < i; k++)
                                            {
                                                if (pos[k] == new Vector2(pos[i].X, pos[i].Y + 1))
                                                    goto Break3;
                                            }
                                            pos[i + 1].Y++;
                                            goto Break2;
                                        }
                                        break;
                                    case 3:
                                        if ((pos[i].X != Game1.width - 1)
                                        && (game.tiles[(int)pos[i].X + 1, (int)pos[i].Y].type == 2)
                                        && ((pos[i].X == Game1.width - 2) || (game.tiles[(int)pos[i].X + 2, (int)pos[i].Y].type != 0))
                                        && ((pos[i].Y == 0) || (game.tiles[(int)pos[i].X + 1, (int)pos[i].Y - 1].type != 0))
                                        && ((pos[i].Y == Game1.height - 1) || (game.tiles[(int)pos[i].X + 1, (int)pos[i].Y + 1].type != 0)))
                                        {
                                            for (int k = 0; k < i; k++)
                                            {
                                                if (pos[k] == new Vector2(pos[i].X + 1, pos[i].Y))
                                                    goto Break3;
                                            }
                                            pos[i + 1].X++;
                                            goto Break2;
                                        }
                                        break;

                                }
                                Break3:;
                                rnd1 = (rnd1 + 1) % 4;
                            }
                            goto Break4;
                            Break2:;
                        }
                        goto Break5;
                        Break4:;
                    }
                    continue;
                    Break5:;
                    for (int i = 0; i < Game1.swampSize; i++)
                    {
                        game.tiles[(int)pos[i].X, (int)pos[i].Y].type = 0;
                    }
                }
                //walls
                for (int i = 0; i < Game1.width; i++)
                {
                    game.tiles[i, 0].up = true;
                    game.tiles[i, Game1.height - 1].down = true;
                }
                for (int i = 0; i < Game1.height; i++)
                {
                    game.tiles[0, i].left = true;
                    game.tiles[Game1.width - 1, i].right = true;
                }
                for (int x = 0; x < Game1.width; x++)
                {
                    for (int y = 0; y < Game1.height; y++)
                    {
                        if (game.tiles[x, y].type == 2)
                        {
                            if (rnd.Next() % game.wallSpawn == 0)
                            {
                                game.tiles[x, y].up = true;
                                if (y != 0)
                                {
                                    game.tiles[x, y - 1].down = true;
                                }
                            }
                            if (rnd.Next() % game.wallSpawn == 0)
                            {
                                game.tiles[x, y].down = true;
                                if (y != Game1.height - 1)
                                {
                                    game.tiles[x, y + 1].up = true;
                                }
                            }
                            if (rnd.Next() % game.wallSpawn == 0)
                            {
                                game.tiles[x, y].left = true;
                                if (x != 0)
                                {
                                    game.tiles[x - 1, y].right = true;
                                }
                            }
                            if (rnd.Next() % game.wallSpawn == 0)
                            {
                                game.tiles[x, y].right = true;
                                if (x != Game1.width - 1)
                                {
                                    game.tiles[x + 1, y].left = true;
                                }
                            }
                        }
                    }
                }
                //home
                for (int i = 0; i < game.players.Length; i++)
                {
                    do
                    {
                        pos[0] = new Vector2(rnd.Next() % Game1.width, rnd.Next() % Game1.height);
                    }
                    while (game.tiles[(int)pos[0].X, (int)pos[0].Y].type != 2);
                    game.tiles[(int)pos[0].X, (int)pos[0].Y].type = 3;
                    game.homes[i].position.X = (int)pos[0].X;
                    game.homes[i].position.Y = (int)pos[0].Y;
                    game.tiles[(int)pos[0].X, (int)pos[0].Y].number = i + 1;
                    game.players[i].pPos = pos[0];
                }
                //дыры
                for (int i = 0; i < 3; i++)
                {
                    do
                    {
                        pos[0] = new Vector2(rnd.Next() % Game1.width, rnd.Next() % Game1.height);
                    }
                    while ((game.tiles[(int)pos[0].X, (int)pos[0].Y].type != 2)
                    || ((pos[0].X == 0 || game.tiles[(int)pos[0].X, (int)pos[0].Y].left || game.tiles[(int)pos[0].X - 1, (int)pos[0].Y].type == 0)
                    && (pos[0].Y == 0 || game.tiles[(int)pos[0].X, (int)pos[0].Y].up || game.tiles[(int)pos[0].X, (int)pos[0].Y - 1].type == 0)
                    && (pos[0].X == Game1.width - 1 || game.tiles[(int)pos[0].X, (int)pos[0].Y].right || game.tiles[(int)pos[0].X + 1, (int)pos[0].Y].type == 0)
                    && (pos[0].Y == Game1.height - 1 || game.tiles[(int)pos[0].X, (int)pos[0].Y].down || game.tiles[(int)pos[0].X, (int)pos[0].Y + 1].type == 0)));
                    game.tiles[(int)pos[0].X, (int)pos[0].Y].type = 4;
                    game.tiles[(int)pos[0].X, (int)pos[0].Y].number = i + 1;
                    game.portals[i] = game.tiles[(int)pos[0].X, (int)pos[0].Y];
                    game.portals[i].position = new Vector2(pos[0].X, pos[0].Y);
                }
                //treasure!
                do
                {
                    pos[0] = new Vector2(rnd.Next() % Game1.width, rnd.Next() % Game1.height);
                }
                while (game.tiles[(int)pos[0].X, (int)pos[0].Y].type != 2);
                game.treasurePos = new Vector2 (pos[0].X,pos[0].Y);
                /*for (int i = 0; i < (game.homes.GetLength(0)) && (game.isMapGood); i++)
                {
                    game.isMapGood = AStarCanGo(game, 1, 1, new Point((int)game.homes[i].position.X, (int)game.homes[i].position.Y), new Point((int)game.treasurePos.X, (int)game.treasurePos.Y));
                }*/
                for (int i = 0; i < (game.homes.GetLength(0)) && (game.isMapGood); i++)
                {
                    game.isMapGood = AStarCanGo(game, 1, 1, new Point((int)game.lastRiverTile.position.X, (int)game.lastRiverTile.position.Y), new Point((int)game.homes[i].position.X, (int)game.homes[i].position.Y));
                }
            }
            while (!game.isMapGood);
        }
        /// <summary>
        /// /
        /// 
        /// 
        /// 
        /// </summary>
        private class AStarNode
        {
            public Point Position { get; set; }
            public int PathLengthFromStart { get; set; }
            public AStarNode CameFrom { get; set; }
            public int HeuristicEstimatePathLength { get; set; }
            public int EstimateFullPathLength
            {
                get
                {
                    return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
                }
            }
        }

        private static List<AStarNode> GetNeighbours(Game1 game, AStarNode pathNode, Point goal, int nullWeight, int wallWeight)
        {
            var result = new List<AStarNode>();

            Point[] neighbourPoints = new Point[4];
            if ((pathNode.Position.X != Game1.width - 1) && (game.tiles[pathNode.Position.X + 1,pathNode.Position.Y].type != 0) && !(game.tiles[pathNode.Position.X, pathNode.Position.Y].right))
            {
                if ((game.tiles[pathNode.Position.X + 1, pathNode.Position.Y].type == 1) && (game.tiles[pathNode.Position.X + 1, pathNode.Position.Y].nextRiver != null))
                {
                    if (game.tiles[pathNode.Position.X + 1, pathNode.Position.Y].nextRiver.nextRiver != null)
                    {
                        neighbourPoints[0] = new Point((int)game.tiles[pathNode.Position.X + 1, pathNode.Position.Y].nextRiver.nextRiver.position.X, (int)game.tiles[pathNode.Position.X + 1, pathNode.Position.Y].nextRiver.nextRiver.position.Y);
                    }
                    else
                    {
                        neighbourPoints[0] = new Point((int)game.tiles[pathNode.Position.X + 1, pathNode.Position.Y].nextRiver.position.X, (int)game.tiles[pathNode.Position.X + 1, pathNode.Position.Y].nextRiver.position.Y);
                    }
                }
                else
                {
                    if (game.tiles[pathNode.Position.X + 1, pathNode.Position.Y].type == 4)
                    {
                        neighbourPoints[0] = new Point((int)game.portals[(game.tiles[pathNode.Position.X + 1, pathNode.Position.Y].number) % (game.portals.Count())].position.X, (int)game.portals[(game.tiles[pathNode.Position.X, pathNode.Position.Y].number) % (game.portals.Count())].position.Y);
                    }
                    else
                    {
                        neighbourPoints[0] = new Point(pathNode.Position.X + 1, pathNode.Position.Y);
                    }
                }
            }
            else
            {
                neighbourPoints[0].X = 10;
                neighbourPoints[0].Y = 10;
            }
            if (pathNode.Position.X != 0 && game.tiles[pathNode.Position.X - 1, pathNode.Position.Y].type != 0 && !game.tiles[pathNode.Position.X, pathNode.Position.Y].left)
            {
                if ((game.tiles[pathNode.Position.X - 1, pathNode.Position.Y].type == 1) && (game.tiles[pathNode.Position.X - 1, pathNode.Position.Y].nextRiver != null))
                {
                    if (game.tiles[pathNode.Position.X - 1, pathNode.Position.Y].nextRiver.nextRiver != null)
                    {
                        neighbourPoints[1] = new Point((int)game.tiles[pathNode.Position.X - 1, pathNode.Position.Y].nextRiver.nextRiver.position.X, (int)game.tiles[pathNode.Position.X - 1, pathNode.Position.Y].nextRiver.nextRiver.position.Y);
                    }
                    else
                    {
                        if (game.tiles[pathNode.Position.X - 1, pathNode.Position.Y].type == 4)
                        {
                            neighbourPoints[1] = new Point((int)game.portals[(game.tiles[pathNode.Position.X - 1, pathNode.Position.Y].number) % (game.portals.Count())].position.X, (int)game.portals[(game.tiles[pathNode.Position.X - 1, pathNode.Position.Y].number) % (game.portals.Count())].position.Y);
                        }
                        else
                        {
                            neighbourPoints[1] = new Point(pathNode.Position.X - 1, pathNode.Position.Y);
                        }
                    }
                }
                else
                    neighbourPoints[1] = new Point(pathNode.Position.X - 1, pathNode.Position.Y);
            }
            else
            {
                neighbourPoints[1].X = 10;
                neighbourPoints[1].Y = 10;
            }
            if ((pathNode.Position.Y != Game1.height - 1) && (game.tiles[pathNode.Position.X, pathNode.Position.Y + 1].type != 0) && !(game.tiles[pathNode.Position.X, pathNode.Position.Y].down))
            {
                if ((game.tiles[pathNode.Position.X, pathNode.Position.Y + 1].type == 1) && (game.tiles[pathNode.Position.X, pathNode.Position.Y + 1].nextRiver != null))
                {
                    if (game.tiles[pathNode.Position.X , pathNode.Position.Y + 1].nextRiver.nextRiver != null)
                    {
                        neighbourPoints[2] = new Point((int)game.tiles[pathNode.Position.X, pathNode.Position.Y + 1].nextRiver.nextRiver.position.X, (int)game.tiles[pathNode.Position.X, pathNode.Position.Y + 1].nextRiver.nextRiver.position.Y);
                    }
                    else
                    {
                        neighbourPoints[2] = new Point((int)game.tiles[pathNode.Position.X, pathNode.Position.Y + 1].nextRiver.position.X, (int)game.tiles[pathNode.Position.X, pathNode.Position.Y + 1].nextRiver.position.Y);
                    }
                }
                else
                {
                    if (game.tiles[pathNode.Position.X, pathNode.Position.Y + 1].type == 4)
                    {
                        neighbourPoints[2] = new Point((int)game.portals[(game.tiles[pathNode.Position.X, pathNode.Position.Y + 1].number) % (game.portals.Count())].position.X, (int)game.portals[(game.tiles[pathNode.Position.X, pathNode.Position.Y + 1].number) % (game.portals.Count())].position.Y);
                    }
                    else
                    {
                        neighbourPoints[2] = new Point(pathNode.Position.X, pathNode.Position.Y + 1);
                    }
                }
            }
            else
            {
                neighbourPoints[2].X = 10;
                neighbourPoints[2].Y = 10;
            }
            if ((pathNode.Position.Y != 0) && (game.tiles[pathNode.Position.X, pathNode.Position.Y - 1].type != 0) && !(game.tiles[pathNode.Position.X, pathNode.Position.Y].up))
            {
                if (game.tiles[pathNode.Position.X, pathNode.Position.Y - 1].type == 1)
                {
                    if (game.tiles[pathNode.Position.X, pathNode.Position.Y - 1].nextRiver.nextRiver != null)
                    {
                        neighbourPoints[3] = new Point((int)game.tiles[pathNode.Position.X, pathNode.Position.Y - 1].nextRiver.nextRiver.position.X, (int)game.tiles[pathNode.Position.X, pathNode.Position.Y - 1].nextRiver.nextRiver.position.Y);
                    }
                    else
                    {
                        neighbourPoints[3] = new Point((int)game.tiles[pathNode.Position.X, pathNode.Position.Y - 1].nextRiver.position.X, (int)game.tiles[pathNode.Position.X, pathNode.Position.Y - 1].nextRiver.position.Y);
                    }
                }
                else
                {
                    if (game.tiles[pathNode.Position.X, pathNode.Position.Y - 1].type == 4)
                    {
                        neighbourPoints[3] = new Point((int)game.portals[(game.tiles[pathNode.Position.X, pathNode.Position.Y - 1].number) % (game.portals.Count())].position.X, (int)game.portals[(game.tiles[pathNode.Position.X, pathNode.Position.Y - 1].number) % (game.portals.Count())].position.Y);
                    }
                    else
                    {
                        neighbourPoints[3] = new Point(pathNode.Position.X, pathNode.Position.Y - 1);
                    }
                }
            }
            else
            {
                neighbourPoints[3].X = 10;
                neighbourPoints[3].Y = 10;
            }
            for (int i = 0; i < 4;i++)
            {
                /* if (point.X < 0 || point.X >= Tiles.GetLength(0))
                     continue;
                 if (point.Y < 0 || point.Y >= Tiles.GetLength(1))
                     continue;*/
                if ((neighbourPoints[i].X != 10) && (neighbourPoints[i].Y != 10))
                {
                    var neighbourNode = new AStarNode()
                    {
                        Position = new Point(neighbourPoints[i].X, neighbourPoints[i].Y),
                        CameFrom = pathNode,
                        PathLengthFromStart = pathNode.PathLengthFromStart +
                        1,
                        HeuristicEstimatePathLength = GetHeuristicPathLength(game, new Point(neighbourPoints[i].X, neighbourPoints[i].Y), goal)
                    };
                    result.Add(neighbourNode);
                }
            }
            return result;
        }

        private static int GetHeuristicPathLength(Game1 game,Point from, Point to)
        {
            int k = Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
            for (int i = 0; i < game.portals.GetLength(0); i++)
            {
                if (Math.Abs(from.X - game.portals[i].position.X) + Math.Abs(from.Y - game.portals[i].position.Y) < k)
                {
                    k = (int)Math.Abs(from.X - game.portals[i].position.X) + (int)Math.Abs(from.Y - game.portals[i].position.Y);
                }
            }
            return k;
        }

        private int GetDistanceBetweenNeighbours(Point one, Point two, int nullWeight, int wallWeight)
        {
            return 1;
        }

        private static List<Point> GetPathForNode(AStarNode pathNode)
        {
            var result = new List<Point>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }
        private static bool AStarCanGo(Game1 game,int nullWeight, int wallWeight, Point start, Point end)
        {
            List<AStarNode> openList = new List<AStarNode>();
            List<AStarNode> closedList = new List<AStarNode>();

            AStarNode startNode = new AStarNode()
            {
                Position = start,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(game,start, end)
            };
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                // Шаг 3.
                /*
                var currentNode = openList.OrderBy(node =>
                  node.EstimateFullPathLength).First();
                  */
                int j = 0;
                for (int i = 0; i < openList.Count; i++)
                {
                    if ((openList[j].EstimateFullPathLength > openList[i].EstimateFullPathLength)
                        || ((openList[j].EstimateFullPathLength == openList[i].EstimateFullPathLength)
                        && (openList[j].HeuristicEstimatePathLength > openList[i].HeuristicEstimatePathLength)))
                    {
                        j = i;
                    }
                }
                var currentNode = openList[j];
                // Шаг 4.
                if (currentNode.Position == end)
                {
                    AStarNode u = currentNode;
                    while (true)
                    {
                        game.pp.Add(u.Position);
                        if (u.CameFrom == null)
                            break;
                        u = u.CameFrom;
                    }
                    return true;
                }
                // Шаг 5.
                openList.Remove(currentNode);
                closedList.Add(currentNode);
                // Шаг 6.
                foreach (var neighbourNode in GetNeighbours(game, currentNode, end, nullWeight, wallWeight))
                {
                    if (neighbourNode.Position == currentNode.Position)
                    {
                        continue;
                    }
                    // Шаг 7.
                    bool count = false;
                    for (int i = 0; i < closedList.Count; i++)
                    {
                        if (closedList[i].Position == neighbourNode.Position)
                        {
                            count = true;
                            break;
                        }
                    }
                    if (count)
                        continue;

                    /*
                    var openNode = openList.FirstOrDefault(node =>
                      node.Position == neighbourNode.Position);
                      */
                    AStarNode openNode = null;
                    for (int i = 0; i < openList.Count; i++)
                    {
                        if (openList[i].Position == neighbourNode.Position)
                        {
                            openNode = openList[i];
                            break;
                        }
                    }
                    // Шаг 8.
                    if (openNode == null)
                    {
                        openList.Add(neighbourNode);
                    }
                    else
                      if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        // Шаг 9.
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }
            return false;
        }
    }
}
