import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { CategorieService } from "../shared/categorie.service";

@Component({
  selector: "app-categoreis",
  templateUrl: "./categories.component.html",
  styles: [],
})
export class CategoriesComponent implements OnInit {
  categorieList;

  constructor(
    private service: CategorieService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.refreshCategorieList();
  }

  refreshCategorieList() {
    this.service.getCategorieList().then((res) => (this.categorieList = res));
  }

  openForEdit(id: number) {
    this.router.navigate(["/categorie/edit/" + id]);
  }
}
