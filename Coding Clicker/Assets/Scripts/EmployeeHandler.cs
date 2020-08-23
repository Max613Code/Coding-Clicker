using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmployeeHandler : MonoBehaviour
{
    public List<EmployeeSOScript> employeeList;

    public GameObject employeeShopPanel;
    public GameObject ownedEmployeesPanel;
    public GameObject EquippedEmployeesPanel;

    public List<EmployeeSOScript> employeeListSorted;

    public GameObject Master;

    private MasterScript MasterScript;

    private void Start()
    {
        MasterScript = Master.GetComponent<MasterScript>();

        employeeListSorted = employeeList.OrderBy(o => (o.salary / o.salaryTimeSecs)).ToList();
    }
}
