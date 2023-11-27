using UnityEngine;

[CreateAssetMenu(fileName ="GameConfig", menuName ="Config/GameConfig", order = 1 )]
public class GameConfig : ScriptableObject
{
    [field: Header("Falling Object Config")]
    [field: SerializeField] public float M_InitialFallingObjectSpeed { get; private set; }
    [field: SerializeField] public float M_Max_Side_Ways { get; private set; }
    [field: SerializeField] public float M_Spawner_height { get; private set; }
    [field: SerializeField] public int M_FallingObjectRotation{ get; private set; }
    [field: SerializeField] public float M_TotalMaxSpped{ get; private set; }
    [field: SerializeField] public float M_TimeToChangeFallPattern{ get; private set; }
   
    [field: Header("m_Player Config")]
    [field: SerializeField] public float M_PlayerCurrentYPosition { get; private set; }
    [field: SerializeField] public float M_MaxDownPosition { get; private set; }
    [field: SerializeField] public float M_MaxSidePosition { get; private set; }
    [field: SerializeField] public float M_PlayerMoveSpeed { get; private set; }
    [field: SerializeField] public float M_Chage_Type_Time { get; private set; }
    [field: SerializeField] public float M_Attack_Time_Interval { get; private set; }
    [field: Header("Power Up Config")]
    [field: SerializeField] public float M_SlowMotionTime { get; private set; }
    [field: SerializeField] public int M_Total_Enemies { get; private set; }
    [field: SerializeField] public float M_Triple_Attack_Time { get; private set; }


    [field: Header("Colors")]
    [field: SerializeField] public Color m_Good_Score_Color { get; private set; }
    [field: SerializeField] public Color m_Bad_Score_Color { get; private set; }
    [field: SerializeField] public Color m_Player_Change_Color { get; private set; }
    [field: SerializeField] public Color m_Triple_Shot_Color { get; private set; }
    [field: SerializeField] public Color m_Slow_Motion_Color { get; private set; }
    [field: SerializeField] public Color m_Kill_Enemy_Color { get; private set; }
}
