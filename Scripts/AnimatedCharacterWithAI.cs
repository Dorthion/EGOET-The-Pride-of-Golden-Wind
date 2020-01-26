using System;
using System.Collections.Generic;

namespace EGOET.Scripts
{
    class AnimatedCharacterWithAI : AnimationCharacter
    {
        public List<Waypoint> Waypoints { get; set; }
        private int nextWaypointIndex = 1;
        public AnimatedCharacterWithAI(string filename, int framesize) : base(filename, framesize) {}

        public override void Update(float deltaTime)
        {
            FollowWayPoints();

            base.Update(deltaTime);
        }

        private void FollowWayPoints()
        {
            if(Waypoints != null && Waypoints.Count != 0)
            {
                Waypoint nextWaypoint = Waypoints[nextWaypointIndex];

                float xDifference = nextWaypoint.XPos - this.Xpos;
                float yDifference = nextWaypoint.YPos - this.Ypos;

                float absXDifference = Math.Abs(xDifference);
                float absYDifference = Math.Abs(yDifference);

                if(absXDifference < 10 && absYDifference < 10)
                {
                    if(nextWaypointIndex < Waypoints.Count - 1)
                    {
                        nextWaypointIndex++;
                    }
                    else 
                    { 
                        nextWaypointIndex = 0; 
                    }
                }

                if(absXDifference > absYDifference)
                {
                    if(xDifference > 0)
                    {
                        this.CurrentState = CharacterState.MovingRight;
                    }

                    if (xDifference < 0)
                    {
                        this.CurrentState = CharacterState.MovingLeft;
                    }
                } 
                else
                {
                    if (yDifference > 0)
                    {
                        this.CurrentState = CharacterState.MovingDown;
                    }

                    if (yDifference < 0)
                    {
                        this.CurrentState = CharacterState.MovingUp;
                    }
                }

            }
        }
    }
}
