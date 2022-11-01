import { Component, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { CartsClient ,CartDto,CartItemDto} from '../web-api-client';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  debug:true
  lists: CartDto[];
  selectedList: CartDto;
  selectedItem: CartItemDto;

  newListEditor: any = {};
  listOptionsEditor: any = {};
  itemDetailsEditor: any = {};
  newListModalRef: BsModalRef;
  listOptionsModalRef: BsModalRef;
  deleteListModalRef: BsModalRef;
  itemDetailsModalRef: BsModalRef;


  constructor( private listsClient: CartsClient,) { }
 
  ngOnInit(): void {
    this.listsClient.getAll().subscribe(
      result => {
        this.lists = result.lists;
        
        if (this.lists.length) {
          this.selectedList = this.lists[0];
        }
      },
      error => console.error(error)
    );
  }

  

 

  newListCancelled(): void {
    
  }

  addList(): void {}
  addItem() :void {}
  editItem(item :any): void {}
  updateItem(item :any): void {}
  
}
