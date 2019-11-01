using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<Type> where Type :new()
{
    private static Type m_instance;

    public static Type instance
    {
        get
        {
            if (m_instance != null) return m_instance;
            m_instance = new Type();
            return m_instance;
        }

        protected set
        {
            m_instance = value;
        }
    }
}
