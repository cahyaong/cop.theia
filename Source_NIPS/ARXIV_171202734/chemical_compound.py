from base64 import b64encode
from io import BytesIO
import os
from PIL.Image import Image


class ChemicalCompound:
    id = -1

    canonical_smiles = None
    flat_depiction = None

    atom_count = -1
    explicit_atom_count = -1

    bond_count = -1
    heavy_bond_count = -1

    def __init__(self):
        pass

    def get_properties(self):
        return {
            'id': self.id,
            'canonical_smiles': self.canonical_smiles,
            'atom_count': self.atom_count,
            'explicit_atom_count': self.explicit_atom_count,
            'bond_count': self.bond_count,
            'heavy_bond_count': self.heavy_bond_count
        }

    def _repr_html_(self):
        table_header = '<tr> <th colspan="2">CHEMICAL COMPOUND</th> </tr> {0}'.format(os.linesep)

        table_body = ''

        properties = self.get_properties()

        for _, key in enumerate(properties):
            table_body += '<tr> <td>--{1}:</td> <td>{2}</td> </tr> {0}'.format(os.linesep, key, properties[key])

        table_body += '<tr align="right"> <td>--flat_depiction:</td> <td>{1}</td> </tr> {0}'.format(
            os.linesep,
            ChemicalCompound._create_embedded_html_image(self.flat_depiction))

        return '<table> {0} {1} {2} </table>'.format(os.linesep, table_header, table_body)

    @staticmethod
    def _create_embedded_html_image(image):
        assert image is not None
        assert isinstance(image, Image)

        stream = BytesIO()
        image.save(stream, format='png')

        return '<img src="data:image/png;base64,{0}" />'.format(b64encode(stream.getvalue()).decode('ascii'))
