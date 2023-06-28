import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { CategoriesComponent } from "./categories/categories.component";
import { CategorieComponent } from "./categories/categorie/categorie.component";
import { BlogComponent } from "./blog/blog.component";
import { PublicationComponent } from "./publications/publication/publication.component";

const routes: Routes = [
  { path: "", redirectTo: "blog", pathMatch: "full" },
  { path: "blog", component: BlogComponent },
  { path: "categories", component: CategoriesComponent },
  {
    path: "categorie",
    children: [
      { path: "", component: CategorieComponent },
      { path: "edit/:id", component: CategorieComponent },
    ],
  },
  {
    path: "publication",
    children: [
      { path: "", component: PublicationComponent },
      { path: "edit/:id", component: PublicationComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
