import { useState } from "react";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogFooter,
  DialogTitle,
  DialogDescription,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Loader2 } from "lucide-react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteProduct } from "@/actions/productsAction";
import type { Product } from "@/types/product";
import { toast } from "sonner";

export default function ProductDeleteModal({
  product,
  setOpen,
  open,
}: {
  product: Product;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  open: boolean;
}) {
  const queryClient = useQueryClient();
  const mutation = useMutation({
    mutationFn: () => deleteProduct(product.id),
    onSuccess: () => {
      setOpen(false);
      toast.success("El producto se ha eliminado exitosamente", {
        id: "delete-product",
      });

      queryClient.invalidateQueries({
        queryKey: ["products"],
      });
    },
    onError: () => {
      setOpen(false);
      toast.error("El producto no se ha eliminado, inténtelo nuevamente", {
        id: "delete-product",
      });
    },
  });

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Desactivar producto</DialogTitle>
          <DialogDescription>
            Estás a punto de eliminar el producto{" "}
            <strong>{product.name}</strong>. Esta acción es{" "}
            <span className="font-semibold text-red-600">irreversible</span> y no
            se podrá deshacer.
          </DialogDescription>
        </DialogHeader>

        <DialogFooter className="flex justify-end gap-2">
          <Button
            variant="secondary"
            onClick={() => setOpen(false)}
            disabled={mutation.isPending}
          >
            Cancelar
          </Button>
          <Button
            variant="destructive"
            onClick={() => mutation.mutate()}
            disabled={mutation.isPending}
          >
            {mutation.isPending && (
              <Loader2 className="h-4 w-4 animate-spin mr-2" />
            )}
            Aceptar
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
