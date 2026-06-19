using ORSchedulingManagement.Models;

namespace ORSchedulingManagement.Repository;

public class UserRepository
{
    private readonly List<User> _users =
    [
        new User
        {
            Id = 1,
            SurgeonName = "Dr. Arjun Sharma",
            Department = "Cardiology",
            OperationRoom = "OR-01",
            SurgeryType = "Bypass Surgery",
            ScheduledTime = "09:00 AM",
            Status = "Scheduled"
        },
        new User
        {
            Id = 2,
            SurgeonName = "Dr. Priya Reddy",
            Department = "Orthopedics",
            OperationRoom = "OR-02",
            SurgeryType = "Knee Replacement",
            ScheduledTime = "11:30 AM",
            Status = "In Progress"
        },
        new User
        {
            Id = 3,
            SurgeonName = "Dr. Meera Iyer",
            Department = "Neurology",
            OperationRoom = "OR-03",
            SurgeryType = "Brain Tumor Removal",
            ScheduledTime = "02:00 PM",
            Status = "Pending"
        },
        new User
        {
            Id = 4,
            SurgeonName = "Dr. Rahul Verma",
            Department = "General Surgery",
            OperationRoom = "OR-04",
            SurgeryType = "Appendectomy",
            ScheduledTime = "04:00 PM",
            Status = "Completed"
        }
    ];

    public List<User> GetAllUsers()
    {
        return _users;
    }

    public User? GetUserById(int id)
    {
        return _users.FirstOrDefault(user => user.Id == id);
    }
}