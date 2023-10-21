    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [ExecuteInEditMode]
    public class WaypointEditor : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private bool _drawGizmos = true;
        public List<WaypointLocationComponent> Waypoints = new();
        
        public List<string> Names { get; private set; }

        public void ImmediatelyUpdateData()
        {
            Validate();
        }

        private void OnTransformChildrenChanged()
        {
            Validate();
        }

        private void Validate()
        {
            Waypoints.Clear();
            Names = new List<string>();
            Waypoints = GetComponentsInChildren<WaypointLocationComponent>().ToList();

            for (var i = 0; i < Waypoints.Count; i++)
            {
                var newName = $"Waypoint_{i}";
                Waypoints[i].name = newName;
                Names.Add(newName);
            }
        }

        private void OnDrawGizmos()
        {
            if (!_drawGizmos)
                return;
            
            if (Waypoints.Count < 2)
                return;
            
            Gizmos.color = Color.green;

            for (var i = 0; i < Waypoints.Count - 1; i++)
                Gizmos.DrawLine(Waypoints[i].transform.position, Waypoints[i + 1].transform.position);
        }
#endif
    }
