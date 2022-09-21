using UnityEngine.InputSystem;

public delegate void VoidHandler();
public delegate void IntHandler(int integer);
public delegate void BulletHandler(Bullet bullet);
public delegate void THandler<T>(T TVarialbe);
public delegate bool BoolOutTHandler<T>(T TVarialbe);
