using Business.Factories;
using Business.Models;
using Data.Entities;
using Data.Repositories;


namespace Business.Services;

public class CustomerService(CustomerRepository customerRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;


    public async Task CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var customerEntity = CustomerFactory.Create(form);
        await _customerRepository.AddAsync(customerEntity!);
    }

    public async Task<IEnumerable<Customer?>> GetCustomerAsync()
    {
        var customerEntities = await _customerRepository.GetAllAsync();

        return customerEntities.Select(CustomerFactory.Create);
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
        return CustomerFactory.Create(customerEntity!);
    }





    public async Task<Customer?> GetCustomerByCustomerNameAsync(string customerName)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
        return CustomerFactory.Create(customerEntity!);
    }

    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
        var existingEntity = await _customerRepository.GetAsync(x => x.Id == customer.Id);
        if (existingEntity != null)
        {
            existingEntity.CustomerName = customer.CustomerName;
            existingEntity.CustomerEmail = (string)customer.CustomerEmail;
            existingEntity.CustomerPhoneNumber = (string)customer.CustomerPhoneNumber;

            await _customerRepository.UpdateAsync(existingEntity);
            return true;
        }
        return false;
    }


    public async Task<bool> DeleteCustomerAsync(int id)
    {
        return await _customerRepository.DeleteAsync(id);
    }

} 
