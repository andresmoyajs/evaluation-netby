import { useState, useEffect } from "react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { Label } from "@/components/ui/label";
import { toast } from "sonner";
import { Loader2, PenIcon, PlusIcon } from "lucide-react";
import { updateProduct } from "@/actions/productsAction";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import type { Product } from "@/types/product";
import { categories } from "@/types/category";

interface ProductEditForm {
  id: number;
  name: string;
  price: number;
  description: string;
  stock: number;
  categoryId: string;
  fotos: File[];
}

export default function ProductEditModal({
  product,
  setOpen,
  open,
}: {
  product: Product;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  open: boolean;
}) {
  const queryClient = useQueryClient();

  const [form, setForm] = useState<ProductEditForm | null>(null);

  useEffect(() => {
    if (product) {
      setForm({
        id: product.id,
        name: product.name,
        price: product.price,
        description: product.description || "",
        stock: product.stock,
        categoryId: product.categoryId?.toString() || "1",
        fotos: [],
      });
    }
  }, [product]);

  const mutation = useMutation({
    mutationFn: async () => {
      if (!form) return;
      const formData = new FormData();
      formData.append("Id", form.id.toString());
      formData.append("Name", form.name);
      formData.append("Price", form.price.toString());
      formData.append("Description", form.description || "");
      formData.append("Stock", form.stock.toString());
      formData.append("CategoryId", form.categoryId || "1");
      form.fotos.forEach((file) => formData.append("Fotos", file));

      return updateProduct(formData);
    },
    onSuccess: () => {
      setOpen(false);
      toast.success("Producto editado exitosamente");
      queryClient.invalidateQueries({ queryKey: ["products"] });
    },
    onError: () => {
      toast.error("Error al editar el producto, inténtalo nuevamente");
    },
  });

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    if (!form) return;
    const { name, value, files } = e.target as any;
    if (name === "fotos") {
      setForm({ ...form, fotos: Array.from(files) });
    } else if (name === "price" || name === "stock") {
      setForm({ ...form, [name]: Number(value) });
    } else {
      setForm({ ...form, [name]: value });
    }
  };

  if (!form) return null;

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogContent className="max-w-lg">
        <DialogHeader>
          <DialogTitle>Editar Producto</DialogTitle>
        </DialogHeader>

        <form
          className="space-y-6"
          onSubmit={(e) => {
            e.preventDefault();

            if (!form.name.trim()) {
              toast.error("El nombre es obligatorio");
              return;
            }
            if (form.price <= 0) {
              toast.error("El precio debe ser mayor a 0");
              return;
            }
            if (!form.description.trim()) {
              toast.error("La descripción es obligatoria");
              return;
            }
            if (form.stock < 0) {
              toast.error("El stock no puede ser negativo");
              return;
            }
            if (!form.categoryId) {
              toast.error("Debe seleccionar una categoría");
              return;
            }

            mutation.mutate();
          }}
        >
          <div className="flex flex-col gap-2">
            <Label>Nombre</Label>
            <Input
              name="name"
              value={form.name}
              onChange={handleChange}
              required
            />
          </div>

          <div className="flex flex-col gap-2">
            <Label>Precio</Label>
            <Input
              type="number"
              name="price"
              value={form.price}
              onChange={handleChange}
              required
              step="0.01"
            />
          </div>

          <div className="flex flex-col gap-2">
            <Label>Descripción</Label>
            <Textarea
              name="description"
              value={form.description}
              onChange={handleChange}
            />
          </div>

          <div className="flex flex-col gap-2">
            <Label>Stock</Label>
            <Input
              type="number"
              name="stock"
              value={form.stock}
              onChange={handleChange}
              required
            />
          </div>

          <div className="flex flex-col gap-2">
            <Label>Categoría</Label>
            <Select
              value={form.categoryId}
              onValueChange={(value) =>
                setForm((prev) =>
                  prev ? { ...prev, categoryId: value } : prev
                )
              }
            >
              <SelectTrigger>
                <SelectValue placeholder="Selecciona categoría" />
              </SelectTrigger>
              <SelectContent>
                {categories.map((cat) => (
                  <SelectItem key={cat.id} value={cat.id.toString()}>
                    {cat.name}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>

          <div className="flex flex-col gap-2">
            <Label>Fotos (puedes agregar nuevas)</Label>
            <Input
              type="file"
              name="fotos"
              onChange={handleChange}
              multiple
              accept="image/*"
            />
          </div>

          <DialogFooter className="flex justify-end gap-2">
            <Button
              type="button"
              variant="secondary"
              onClick={() => setOpen(false)}
              disabled={mutation.isPending}
            >
              Cancelar
            </Button>
            <Button type="submit" disabled={mutation.isPending}>
              {mutation.isPending && (
                <Loader2 className="h-4 w-4 animate-spin mr-2" />
              )}
              <PenIcon className="h-4 w-4 mr-1" />
              Guardar Cambios
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
