﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Starship.Core.Models;
using Starship.Core.Models.Abstracts;
using Starship.Core.Services.Interfaces;
using KdTree.Math;
using Starship.Core.Dtos;

namespace Starship.Core.Services
{
    public class TravelService : ITravelService
    {
        //Assumption made about positions that they get more specific from L to R
        //Implmented using rough distance calculation, getting more accurate until nearest position is unique distance
        public BaseSpaceObject FindNearestObject(Position currentPos, IList<BaseSpaceObject> spaceObjects)
        {
            var cX = currentPos.X;
            var cY = currentPos.Y;
            var cZ= currentPos.Z;

            var result = spaceObjects
                .Select(t =>
                {
                    var tX = t.Position.X;
                    var tY = t.Position.Y;
                    var tZ = t.Position.Z;
                    return CloseEstimateToDistance(cX.Area1, tX.Area1, cY.Area1, tY.Area1, cZ.Area1, tZ.Area1, t);
                })
                .OrderBy(t => t.Distance)
                .ToList();

            if (result.Count() > 1 && !IsNearestDistanceUnique(result))
            {
                result = spaceObjects
                .Select(t =>
                {
                    var tX = t.Position.X;
                    var tY = t.Position.Y;
                    var tZ = t.Position.Z;
                    return CloseEstimateToDistance(cX.Area2, tX.Area2, cY.Area2, tY.Area2, cZ.Area2, tZ.Area2, t);
                })
                .OrderBy(t => t.Distance)
                .ToList();

                if (!IsNearestDistanceUnique(result))
                {
                    result = spaceObjects
                    .Select(t =>
                    {
                        var tX = t.Position.X;
                        var tY = t.Position.Y;
                        var tZ = t.Position.Z;
                        return CloseEstimateToDistance(cX.Area3, tX.Area3, cY.Area3, tY.Area3, cZ.Area3, tZ.Area3, t);
                    })
                    .OrderBy(t => t.Distance)
                    .ToList();

                    if (!IsNearestDistanceUnique(result))
                    {
                        result = spaceObjects
                        .Select(t =>
                        {
                            var tX = t.Position.X;
                            var tY = t.Position.Y;
                            var tZ = t.Position.Z;
                            return CloseEstimateToDistance(cX.Area4, tX.Area4, cY.Area4, tY.Area4, cZ.Area4, tZ.Area4, t);
                        })
                        .OrderBy(t => t.Distance)
                        .ToList();
                    }
                }
            }

            return result.First().Object;
        }

        private bool IsNearestDistanceUnique(List<SortResult> result)
        {
            return result[0].Distance == result[1].Distance;
        }
        
        private SortResult CloseEstimateToDistance(int sX, int tX, int sY, int tY, int sZ, int tZ, BaseSpaceObject bObject)
        {
            return new SortResult
            {
                Object = bObject,
                Distance = Math.Pow(tX - sX, 2) + Math.Pow(tY - sY, 2) + Math.Pow(tZ - sZ, 2)
            };
        }
    }
}