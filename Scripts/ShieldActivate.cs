using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class ShieldActivate : MonoBehaviour
{
  public float ImpactLife;
  private Vector4[] points;
  private Material m_material;
  private List<Vector4> Hitpoints;
  private MeshRenderer m_meshRenderer;
  private float time;

  private void Start()
  {
    this.time = Time.time;
    this.points = new Vector4[30];
    this.Hitpoints = new List<Vector4>();
    this.m_meshRenderer = this.GetComponent<MeshRenderer>();
    this.m_material = this.m_meshRenderer.material;
  }

  private void Update()
  {
    this.m_material.SetVectorArray("_Points", this.points);
    this.Hitpoints = this.Hitpoints.Select<Vector4, Vector4>((Func<Vector4, Vector4>) (s => new Vector4(s.x, s.y, s.z, s.w + Time.deltaTime / this.ImpactLife))).Where<Vector4>((Func<Vector4, bool>) (w => (double) w.w <= 1.0)).ToList<Vector4>();
    if ((double) Time.time > (double) this.time + 0.10000000149011612)
    {
      this.time = Time.time;
      this.AddEmpty();
    }
    this.Hitpoints.ToArray().CopyTo((Array) this.points, 0);
  }

  public void AddHitObject(Vector3 position)
  {
    position -= this.transform.position;
    position = position.normalized / 2f;
    this.Hitpoints.Add(new Vector4(position.x, position.y, position.z, 0.0f));
  }

  public void AddEmpty() => this.Hitpoints.Add(new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
}
