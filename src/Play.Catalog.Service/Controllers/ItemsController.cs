using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        

        private readonly ItemsRepository itemsRepository;

        public ItemsController(ItemsRepository _itemsRepository)
        {
            itemsRepository = _itemsRepository;
            
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await itemsRepository.GetAllAsync()).Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(ObjectId id)
        {
            var item =  (await itemsRepository.GetAsync(id)).AsDto();
            
            if(item == null){
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto){
            var item = new Item {Id = ObjectId.GenerateNewId(), Name= createItemDto.Name, Description = createItemDto.Description, Price = createItemDto.Price, CreatedDate = DateTimeOffset.Now};
            await itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new {id = item.Id}, item.AsDto());
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> PutAsync(ObjectId id, UpdateItemDto updateItemDto){
            var existingItem = await itemsRepository.GetAsync(id);

            if(existingItem == null){
                return NotFound();
            }

                existingItem.Name = updateItemDto.Name;
                existingItem.Description = updateItemDto.Description;
                existingItem.Price = updateItemDto.Price;

            await itemsRepository.UpdateAsync(existingItem);
            return NoContent();

        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteAsync(ObjectId id){
            var item = await itemsRepository.GetAsync(id);

            if(item == null){
                return NotFound();
            }
            await itemsRepository.RemoveAsync(item.Id.Value);

            return NoContent();
        }

    }
    
}