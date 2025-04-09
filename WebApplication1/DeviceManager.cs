using System.Collections.Generic;
using System;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1 
{
    public class DeviceManager
    {
        private static List<Device> devices = new List<Device>();
        private const int MaxDevices = 15;

        
        public DeviceManager() { }

        
        public List<DeviceShortInfoResponse> GetAllDevicesShortInfo()
        {
            return devices.Select(d => new DeviceShortInfoResponse
            {
                Id = d.Id,
                Name = d.Name,
                Type = d.GetType().Name 
            }).ToList();
        }

        
        public Device? GetDeviceById(string id)
        {
            return devices.FirstOrDefault(d => d.Id == id);
        }

        
        public void AddDevice(Device device)
        {
            if (devices.Count >= MaxDevices)
                throw new InvalidOperationException("Storage is full. Cannot add more devices.");
            if (devices.Any(d => d.Id == device.Id))
                throw new ArgumentException($"Device with ID '{device.Id}' already exists.");
            devices.Add(device);
        }

        
        public void UpdateDevice(string id, Device updatedDevice)
        {
            var existingDeviceIndex = devices.FindIndex(d => d.Id == id);
            if (existingDeviceIndex == -1)
            {
                throw new ArgumentException($"Device with ID '{id}' not found.");
            }
            updatedDevice.Id = id;
            devices[existingDeviceIndex] = updatedDevice;
        }


        
        public void RemoveDevice(string id)
        {
            var deviceToRemove = devices.FirstOrDefault(d => d.Id == id);
            if (deviceToRemove == null)
            {
                throw new ArgumentException($"Device with ID '{id}' not found.");
            }
            devices.Remove(deviceToRemove);
        }

        public int GetDeviceCount() => devices.Count;
    }
}